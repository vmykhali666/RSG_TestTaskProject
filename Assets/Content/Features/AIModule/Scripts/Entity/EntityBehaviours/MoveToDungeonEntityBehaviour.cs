using System;
using Content.Features.AIModule.Scripts.Entity.EntityContext;
using Content.Features.GameFlowStateMachineModule.Scripts;
using Content.Features.PlayerData.Scripts.Datas;
using UnityEngine;

namespace Content.Features.AIModule.Scripts.Entity.EntityBehaviours
{
    public class MoveToDungeonEntityBehaviour : IEntityBehaviour
    {
        private BaseEntityContext _entityContext;
        private Vector3 _teleportPosition;
        private GameFlowStateMachine _gameFlowStateMachine;

        public event Action OnBehaviorEnd;

        public MoveToDungeonEntityBehaviour(GameFlowStateMachine gameFlowStateMachine) =>
            _gameFlowStateMachine = gameFlowStateMachine;

        public void InitContext(BaseEntityContext entityContext) =>
            _entityContext = entityContext;

        public void SetTelepotPosition(Vector3 teleportPosition) =>
            _teleportPosition = teleportPosition;

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
                TeleportToDungeon();
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
                navContext.NavMeshAgent.SetDestination(_teleportPosition);
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
        }

        private bool IsNearTheTarget()
        {
            if (_entityContext.EntityData is IInteractableData interactableData &&
                _entityContext is IDamageableContext damageable)
            {
                var distance = Vector3.Distance(damageable.EntityDamageable.Position, _teleportPosition);
                return distance <=
                       interactableData.InteractDistance;
            }

            return true;
        }


        private void TeleportToDungeon()
        {
            _gameFlowStateMachine.Enter<EnterDungeonFlowState>();
            StopMoving();
            OnBehaviorEnd?.Invoke();
        }
    }
}