using System;
using Content.Features.AIModule.Scripts.Entity.EntityContext;
using Content.Features.LootModule.Scripts;
using Content.Features.PlayerData.Scripts.Datas;
using Content.Features.StorageModule.Scripts.Constraints;
using UnityEngine;

namespace Content.Features.AIModule.Scripts.Entity.EntityBehaviours
{
    public class GatherLootEntityBehaviour : IEntityBehaviour
    {
        private BaseEntityContext _entityContext;
        private Loot _loot;
        private readonly ILootService _lootService;
        private readonly IStorageConstraintService _constraintService;

        public event Action OnBehaviorEnd;

        public GatherLootEntityBehaviour(ILootService lootService, IStorageConstraintService constraintService)
        {
            _lootService = lootService;
            _constraintService = constraintService;
        }

        public void InitContext(BaseEntityContext entityContext) =>
            _entityContext = entityContext;

        public void SetLoot(Loot loot) =>
            _loot = loot;

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
                CollectLoot();
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
                navContext.NavMeshAgent.SetDestination(_loot.transform.position);
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
            if (_entityContext.EntityData is IInteractableData interactable &&
                _entityContext is IDamageableContext damageable)
            {
                return Vector3.Distance(damageable.EntityDamageable.Position, _loot.transform.position) <=
                       interactable.InteractDistance;
            }

            return true;
        }

        private void CollectLoot()
        {
            if (_entityContext is IStorageContext storageContext)
            {
                var storageConstraintResult = _constraintService.CheckConstraints(_loot, storageContext.Storage);

                if (storageConstraintResult.IsValid)
                {
                    _lootService.CollectLoot(_loot, storageContext.Storage);
                    _loot.DestroyLoot();
                }
                else
                {
                    Debug.Log($"Loot can't be collected: {storageConstraintResult.Message}");
                }
            }

            StopMoving();
            OnBehaviorEnd?.Invoke();
        }
    }
}