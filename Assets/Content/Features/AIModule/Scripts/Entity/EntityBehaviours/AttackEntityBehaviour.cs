using System;
using Content.Features.AIModule.Scripts.Entity.Datas;
using Content.Features.DamageablesModule.Scripts;
using UnityEngine;

namespace Content.Features.AIModule.Scripts.Entity.EntityBehaviours
{
    public class AttackEntityBehaviour : IEntityBehaviour
    {
        private EntityContext _entityContext;
        private IDamageable _targetDamageable;

        public event Action OnBehaviorEnd;

        public void InitContext(EntityContext entityContext) =>
            _entityContext = entityContext;

        public void SetTarget(IDamageable damageable) =>
            _targetDamageable = damageable;

        public void Start()
        {
            var movable = _entityContext.EntityData as IMovableData;
            _entityContext.NavMeshAgent.speed = movable?.Speed ?? 0;
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
            _entityContext.NavMeshAgent.SetDestination(_targetDamageable.Position);
        }

        private void StopMoving() =>
            _entityContext.NavMeshAgent.ResetPath();

        private bool IsNearTheTarget()
        {
            if (_targetDamageable.IsActive is false)
                return false;

            var attackableData = _entityContext.EntityData as IAttackableData;
            return Vector3.Distance(_entityContext.EntityDamageable.Position, _targetDamageable.Position) <=
                   (attackableData?.AttackDistance ?? 0);
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