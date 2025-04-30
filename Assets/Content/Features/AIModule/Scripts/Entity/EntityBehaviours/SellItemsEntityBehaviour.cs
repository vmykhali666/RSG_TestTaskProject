using System;
using Content.Features.AIModule.Scripts.Entity.EntityContext;
using Content.Features.DamageablesModule.Scripts;
using Content.Features.PlayerData.Scripts.Datas;
using Content.Features.ShopModule.Scripts;
using Content.Features.StorageModule.Scripts;
using UnityEngine;

namespace Content.Features.AIModule.Scripts.Entity.EntityBehaviours
{
    public class SellItemsEntityBehaviour : IEntityBehaviour
    {
        private BaseEntityContext _entityContext;
        private Trader _trader;
        private readonly CurrencyPaymentService _currencyPaymentService;


        public SellItemsEntityBehaviour(CurrencyPaymentService currencyPaymentService)
        {
            _currencyPaymentService = currencyPaymentService;
        }

        public event Action OnBehaviorEnd;

        public void InitContext(BaseEntityContext entityContext) =>
            _entityContext = entityContext;

        public void SetTrader(Trader trader) =>
            _trader = trader;

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
                SellItems();
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
                navContext.NavMeshAgent.SetDestination(_trader.transform.position);
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
        }

        private bool IsNearTheTarget()
        {
            if (_entityContext.EntityData is IInteractableData interactableData &&
                _entityContext is IDamageableContext damageable)
            {
                return Vector3.Distance(damageable.EntityDamageable.Position, _trader.transform.position) <=
                       interactableData.InteractDistance;
            }
            
            return true;
        }

        private void SellItems()
        {
            if (_entityContext is IStorageContext storageContext)
            {
                var defaultItems = storageContext.Storage.GetAllItems<SellableItem>();
                var currencyAmount = _trader.SellItemsFromStorage(defaultItems, storageContext.Storage);
                _currencyPaymentService.AddCurrency(currencyAmount);
            }

            StopMoving();
            OnBehaviorEnd?.Invoke();
        }
    }
}