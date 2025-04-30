using System;
using Content.Features.AIModule.Scripts.Entity.EntityContext;
using Content.Features.DamageablesModule.Scripts;
using Content.Features.PlayerData.Scripts.Datas;
using UnityEngine;

namespace Content.Features.AIModule.Scripts.Entity.EntityBehaviours
{
    public class AttackEntityBehaviour : IEntityBehaviour
    {
        private BaseEntityContext _entityContext;
        private IDamageable _targetDamageable;

        public event Action OnBehaviorEnd;

        public void InitContext(BaseEntityContext entityContext) =>
            _entityContext = entityContext;

        public void SetTarget(IDamageable damageable) =>
            _targetDamageable = damageable;


        public void Start()
        {
            if (_entityContext.EntityData is IMovableData movable && _entityContext is INavigationContext navContext)
            {
                navContext.NavMeshAgent.speed = movable.Speed;
            }

            _entityContext.EntityAnimator.OnAttackTriggered += OnAttackTriggered;
        }

        public void Process()
        {
            if (_targetDamageable.IsActive is false)
            {
                OnBehaviorEnd?.Invoke();
                return;
            }

            if (IsNearTheTarget())
                StartAttacking();
            else
                MoveToTarget();
        }

        public void Stop() =>
            _entityContext.EntityAnimator.OnAttackTriggered -= OnAttackTriggered;

        private void MoveToTarget()
        {
            if (_targetDamageable.IsActive is false)
                return;

            _entityContext.EntityAnimator.SetIsAttacking(false);
            if (_entityContext is INavigationContext navContext)
            {
                navContext.NavMeshAgent.SetDestination(_targetDamageable.Position);
            }
        }

        private void StopMoving()
        {
            if (_entityContext is INavigationContext navContext)
            {
                navContext.NavMeshAgent.ResetPath();
            }
        }

        private bool IsNearTheTarget()
        {
            if (_targetDamageable.IsActive is false)
                return false;

            if (_entityContext.EntityData is IAttackableData attackableData &&
                _entityContext is IDamageableContext damageable)
            {
                var distance = Vector3.Distance(damageable.EntityDamageable.Position,
                    _targetDamageable.Position);
                return distance <=
                       attackableData.AttackDistance;
            }

            return true;
        }

        private void StartAttacking()
        {
            _entityContext.EntityAnimator.SetIsAttacking(true);
            StopMoving();
        }

        private void OnAttackTriggered()
        {
            var attackableData = _entityContext.EntityData as IAttackableData;
            _targetDamageable.Damage(attackableData?.Damage ?? 0);
        }
    }
}