using System;
using Content.Features.ShopModule.Scripts;
using Content.Features.StorageModule.Scripts;
using Content.Features.StorageModule.Scripts.Constraints;
using UnityEngine;

namespace Content.Features.AIModule.Scripts.Entity.EntityBehaviours
{
    public class BuyHealEntityBehaviour : IEntityBehaviour
    {
        private EntityContext _entityContext;
        private readonly PlayerStorageService _playerStorageService;
        private readonly IItemFactory _itemFactory;
        private readonly IStorageConstraintService _constraintService;
        private Transform _targetTransform;
        private readonly CurrencyPaymentService _currencyService;

        public event Action OnBehaviorEnd;

        public BuyHealEntityBehaviour(PlayerStorageService storageService, IItemFactory itemFactory,
            IStorageConstraintService storageConstraintService, CurrencyPaymentService currencyPaymentService)
        {
            _playerStorageService = storageService;
            _itemFactory = itemFactory;
            _currencyService = currencyPaymentService;
            _constraintService = storageConstraintService;
        }

        public void InitContext(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public void Start()
        {
            _entityContext.EntityAnimator.SetIsAttacking(false);
            _entityContext.NavMeshAgent.ResetPath();
            _entityContext.NavMeshAgent.speed = _entityContext.EntityData.Speed;
        }

        public void Process()
        {
            if (IsNearTheTarget())
                BuyHeal();
            else
                MoveToTarget();
        }

        private bool IsNearTheTarget() =>
            Vector3.Distance(_entityContext.EntityDamageable.Position, _targetTransform.position) <=
            _entityContext.EntityData.InteractDistance;

        private void MoveToTarget() =>
            _entityContext.NavMeshAgent.SetDestination(_targetTransform.position);

        private void StopMoving() =>
            _entityContext.NavMeshAgent.ResetPath();

        public void Stop()
        {
        }

        private void BuyHeal()
        {
            var item = _itemFactory.GetItem<IHealable, Item>();
            if (item != null && item is IBuyable buyable && _currencyService.CanPay(buyable.BuyPrice))
            {
                var storageConstraintResult = _constraintService.CheckConstraints(item, _entityContext.Storage);

                if (storageConstraintResult.IsValid)
                {
                    _playerStorageService.Storage.AddItem(item);
                    _currencyService.RemoveCurrency(buyable.BuyPrice);
                }
                else
                {
                    Debug.Log("Not enough space in storage");
                }
            }

            StopMoving();
            OnBehaviorEnd?.Invoke();
        }

        public void SetTarget(Transform transform)
        {
            _targetTransform = transform;
        }
    }
}