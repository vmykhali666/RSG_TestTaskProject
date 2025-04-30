using System;
using Content.Features.AIModule.Scripts.Entity.EntityContext;
using Content.Features.DamageablesModule.Scripts;
using Content.Features.PlayerData.Scripts.Datas;
using Content.Features.ShopModule.Scripts;
using Content.Features.StorageModule.Scripts;
using Content.Features.StorageModule.Scripts.Constraints;
using UnityEngine;

namespace Content.Features.AIModule.Scripts.Entity.EntityBehaviours
{
    public class BuyHealEntityBehaviour : IEntityBehaviour
    {
        private BaseEntityContext _entityContext;
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

        public void InitContext(BaseEntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public void Start()
        {
            _entityContext.EntityAnimator.SetIsAttacking(false);
            if (_entityContext.EntityData is IMovableData movable && _entityContext is INavigationContext navContext)
            {
                navContext.NavMeshAgent.ResetPath();
                navContext.NavMeshAgent.speed = movable.Speed;
            }
        }

        public void Process()
        {
            if (IsNearTheTarget())
                BuyHeal();
            else
                MoveToTarget();
        }

        private bool IsNearTheTarget()
        {
            if (_entityContext.EntityData is IAttackableData attackableData &&
                _entityContext is IDamageableContext damageable)
            {
                return Vector3.Distance(damageable.EntityDamageable.Position, damageable.EntityDamageable.Position) <=
                       attackableData.AttackDistance;
            }

            return true;
        }

        private void MoveToTarget()
        {
            if (_entityContext is INavigationContext navContext)
            {
                navContext.NavMeshAgent.SetDestination(_targetTransform.position);
            }
        }

        private void StopMoving()
        {
            if (_entityContext is INavigationContext navContext)
            {
                navContext.NavMeshAgent.ResetPath();
            }
        }

        public void Stop()
        {
        }

        private void BuyHeal()
        {
            var item = _itemFactory.GetItem<IHealable, Item>();
            if (item is IBuyable buyable && _currencyService.CanPay(buyable.BuyPrice) &&
                _entityContext is IStorageContext storageContext)
            {
                var storageConstraintResult = _constraintService.CheckConstraints(item, storageContext.Storage);

                if (storageConstraintResult.IsValid)
                {
                    _playerStorageService.Storage.AddItem(item);
                    _currencyService.RemoveCurrency(buyable.BuyPrice);
                }
                else
                {
                    Debug.Log(storageConstraintResult.Message);
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