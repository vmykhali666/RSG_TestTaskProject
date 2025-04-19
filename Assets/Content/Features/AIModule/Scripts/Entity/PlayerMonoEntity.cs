using System.Collections;
using Content.Features.AIModule.Scripts.Entity.Datas;
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
        private void InjectDependencies(PlayerStorageService playerStorageService,
            BasePersistor<PlayerPersistentData> persistor)
        {
            _playerStorageService = playerStorageService;
            _playerPersistor = persistor;
        }


        protected override EntityData InitializeEntytiData()
        {
            var entityData = _entityDataService.GetEntityData<PlayerEntityData>();
            return entityData;
        }

        protected override IStorage CreateStorage()
        {
            return _playerStorageService.Storage;
        }

        public override void Start()
        {
            base.Start();
            var playerData = _playerPersistor.GetDataModel();
            _entityContext.EntityDamageable.SetHealth(playerData.CurrentHealth);
            var playerDataModel = _playerPersistor.GetDataModel();
            if (playerDataModel != null)
            {
                _playerPersistor.UpdateModel(new PlayerPersistentData()
                {
                    CurrentHealth = playerDataModel.CurrentHealth,
                    MaxHealth = playerDataModel.MaxHealth,
                    Currency = playerDataModel.Currency
                });
            }

            _entityContext.EntityDamageable.OnHealthChanged += OnHealthChanged;
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
                _entityContext.EntityDamageable.SetHealth(_entityContext.EntityDamageable.CurrentHealth +
                                                          healable.HealAmount);
                StartCoroutine(HealPlayerCoroutine());
            }
        }

        private IEnumerator HealPlayerCoroutine()
        {
            _healingEffect.SetActive(true);
            yield return new WaitForSeconds(_healingShowTime);
            _healingEffect.SetActive(false);
        }

        private void OnHealthChanged(float currentHealth, float maxHealth)
        {
            var data = _playerPersistor.GetDataModel();
            _playerPersistor.UpdateModel(new PlayerPersistentData()
            {
                CurrentHealth = currentHealth,
                MaxHealth = maxHealth,
                Currency = data.Currency
            });
            _playerPersistor.SaveData();
            _signalBus.Fire(new PlayerHealthChangedSignal(currentHealth, maxHealth));
        }
    }
}