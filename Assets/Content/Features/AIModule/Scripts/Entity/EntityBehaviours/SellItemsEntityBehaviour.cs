using System;
using System.Linq;
using Content.Features.PlayerData.Scripts;
using Content.Features.ShopModule.Scripts;
using Content.Features.StorageModule.Scripts;
using Core.GlobalSignalsModule.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Content.Features.AIModule.Scripts.Entity.EntityBehaviours
{
    public class SellItemsEntityBehaviour : IEntityBehaviour
    {
        private EntityContext _entityContext;
        private Trader _trader;
        private readonly CurrencyPaymentService _currencyPaymentService;


        public SellItemsEntityBehaviour(CurrencyPaymentService currencyPaymentService)
        {
            _currencyPaymentService = currencyPaymentService;
        }

        public event Action OnBehaviorEnd;

        public void InitContext(EntityContext entityContext) =>
            _entityContext = entityContext;

        public void SetTrader(Trader trader) =>
            _trader = trader;

        public void Start() =>
            _entityContext.NavMeshAgent.speed = _entityContext.EntityData.Speed;

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

        private void MoveToTarget() =>
            _entityContext.NavMeshAgent.SetDestination(_trader.transform.position);

        private void StopMoving() =>
            _entityContext.NavMeshAgent.ResetPath();

        private bool IsNearTheTarget() =>
            Vector3.Distance(_entityContext.EntityDamageable.Position, _trader.transform.position) <=
            _entityContext.EntityData.InteractDistance;

        private void SellItems()
        {
            var defaultItems = _entityContext.Storage.GetAllItems<SellableItem>();
            var currencyAmount = _trader.SellItemsFromStorage(defaultItems, _entityContext.Storage);
            _currencyPaymentService.AddCurrency(currencyAmount);

            StopMoving();
            OnBehaviorEnd?.Invoke();
        }
    }
}