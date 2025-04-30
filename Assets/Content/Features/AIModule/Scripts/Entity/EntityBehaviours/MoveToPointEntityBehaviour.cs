using System;
using Content.Features.AIModule.Scripts.Entity.EntityContext;
using Content.Features.PlayerData.Scripts.Datas;
using UnityEngine;

namespace Content.Features.AIModule.Scripts.Entity.EntityBehaviours
{
    public class MoveToPointEntityBehaviour : IEntityBehaviour
    {
        private BaseEntityContext _entityContext;
        private Vector3 _moveToPosition;

        public event Action OnBehaviorEnd;

        public void InitContext(BaseEntityContext entityContext) =>
            _entityContext = entityContext;

        public void SetMoveToPosition(Vector3 teleportPosition) =>
            _moveToPosition = teleportPosition;

        public void Start()
        {
            if (_entityContext.EntityData is IMovableData movable && _entityContext is INavigationContext navContext)
            {
                navContext.NavMeshAgent.speed = movable.Speed;
            }
        }


        public void Process()
        {
            if (IsNearTheTarget())
                StopMoving();
            else
                MoveToTarget();
        }

        public void Stop()
        {
        }

        private void MoveToTarget()
        {
            if (_entityContext is INavigationContext navContext)
            {
                navContext.NavMeshAgent.SetDestination(_moveToPosition);
            }
            else
            {
                Debug.LogError($"EntityContext {_entityContext} does not implement {nameof(INavigationContext)}");
            }
        }

        private void StopMoving()
        {
            if (_entityContext is INavigationContext navContext)
            {
                navContext.NavMeshAgent.ResetPath();
            }
            else
            {
                Debug.LogError($"EntityContext {_entityContext} does not implement {nameof(INavigationContext)}");
            }

            OnBehaviorEnd?.Invoke();
        }

        private bool IsNearTheTarget()
        {
            if (_entityContext.EntityData is IInteractableData interactable &&
                _entityContext is IDamageableContext damageable)
            {
                return Vector3.Distance(damageable.EntityDamageable.Position, _moveToPosition) <=
                       interactable.InteractDistance;
            }

            return true;
        }
    }
}