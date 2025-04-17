using System;
using Content.Features.PlayerData.Scripts;
using Content.Features.ShopModule.Scripts;
using UnityEngine;

namespace Content.Features.AIModule.Scripts.Entity.EntityBehaviours {
    public class SellItemsEntityBehaviour : IEntityBehaviour {
        private readonly BasePersistor<PlayerPersistentData> _playerDataPersistor;
        private EntityContext _entityContext;
        private Trader _trader;
        
        
        public SellItemsEntityBehaviour(BasePersistor<PlayerPersistentData> playerPersistor)
        {
            _playerDataPersistor = playerPersistor;
        }
        
        public event Action OnBehaviorEnd;
        public void InitContext(EntityContext entityContext) =>
            _entityContext = entityContext;
        
        public void SetTrader(Trader trader) =>
            _trader = trader;

        public void Start() =>
            _entityContext.NavMeshAgent.speed = _entityContext.EntityData.Speed;

        public void Process() {
            if(IsNearTheTarget())
                SellItems();
            else
                MoveToTarget();
        }

        public void Stop() { }

        private void MoveToTarget() =>
            _entityContext.NavMeshAgent.SetDestination(_trader.transform.position);

        private void StopMoving() =>
            _entityContext.NavMeshAgent.ResetPath();

        private bool IsNearTheTarget() =>
            Vector3.Distance(_entityContext.EntityDamageable.Position, _trader.transform.position) <= _entityContext.EntityData.InteractDistance;

        private void SellItems() {
            var currencyAmount = _trader.SellAllItemsFromStorage(_entityContext.Storage);
            //TODO: works with persistor from someService and remove it from here and fire signal instead of this code
            var playerData = _playerDataPersistor.GetDataModel();
            playerData.Currency += currencyAmount;
            _playerDataPersistor.UpdateModel(playerData);
            _playerDataPersistor.SaveData();
            
            StopMoving();
            OnBehaviorEnd?.Invoke();
        }
    }
}