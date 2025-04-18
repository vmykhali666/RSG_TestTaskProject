using System.Collections;
using Content.Features.DamageablesModule.Scripts.Signals;
using Content.Features.PlayerData.Scripts;
using Content.Features.StorageModule.Scripts;
using Core.GlobalSignalsModule.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Content.Features.AIModule.Scripts.Entity
{
    public class PlayerMonoEntity : MonoEntity
    {
        [SerializeField] private GameObject _healingEffect;
        [SerializeField] private float _healingShowTime = 1f;
        private PlayerStorageService _playerStorageService;
        private BasePersistor<PlayerPersistentData> _playerPersistor;
        private IEnumerator _healingCoroutine;

        [Inject]
        private void InjectDependencies(PlayerStorageService playerStorageService, BasePersistor<PlayerPersistentData> persistor)
        {
            _playerStorageService = playerStorageService;
            _playerPersistor = persistor;
        }
        
        
        protected override IStorage CreateStorage()
        {
            return _playerStorageService.Storage;
        }
        
        public override void Start()
        {
            base.Start();
            PlayerDataPersistance();
            _signalBus.Subscribe<HealPlayerSignal>(OnHealPlayer);
        }
        
        private void OnDestroy()
        {
            _signalBus.Unsubscribe<HealPlayerSignal>(OnHealPlayer);
            _entityContext.EntityDamageable.OnHealthChanged -= OnHealthChanged;
            base.OnDestroy();
        }

        private void OnHealPlayer(HealPlayerSignal obj)
        {
            if (obj.Item is IHealable healable)
            {
                _entityContext.EntityDamageable.SetHealth(_entityContext.EntityDamageable.CurrentHealth + healable.HealAmount);
                StartCoroutine(HealPlayerCoroutine());
            }
        }

        private IEnumerator HealPlayerCoroutine()
        {
            _healingEffect.SetActive(true);
            yield return new WaitForSeconds(_healingShowTime);
            _healingEffect.SetActive(false);
        }

        private void PlayerDataPersistance()
        {
            if (_entityType == EntityType.Player)
            {
                var playerData = _playerPersistor.GetDataModel();
                if (playerData == null)
                {
                    // TODO: Add default values for player data and remove this block from MonoEntity
                    playerData = new PlayerPersistentData
                    {
                        MaxHealth = _entityContext.EntityDamageable.MaxHealth,
                        CurrentHealth = _entityContext.EntityDamageable.CurrentHealth,
                        Currency = 0
                    };
                    _playerPersistor.UpdateModel(playerData);
                    _playerPersistor.SaveData();
                }
                _entityContext.EntityDamageable.SetHealth(playerData.CurrentHealth);
                _entityContext.EntityDamageable.OnHealthChanged += OnHealthChanged;
            }
        }
        
        private void OnHealthChanged(float currentHealth, float maxHealth)
        {
            _playerPersistor.UpdateModel(new PlayerPersistentData()
            {
                CurrentHealth = currentHealth,
                MaxHealth = maxHealth
            });
            _playerPersistor.SaveData();
            _signalBus.Fire(new PlayerHealthChangedSignal(currentHealth, maxHealth));
        }
    }
}