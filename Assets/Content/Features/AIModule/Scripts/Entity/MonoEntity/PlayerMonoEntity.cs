using System.Collections;
using Content.Features.AIModule.Scripts.Entity.EntityContext;
using Content.Features.DamageablesModule.Scripts;
using Content.Features.DamageablesModule.Scripts.Signals;
using Content.Features.EntityAnimatorModule.Scripts;
using Content.Features.GameFlowStateMachineModule.Scripts;
using Content.Features.PlayerData.Scripts;
using Content.Features.PlayerData.Scripts.Datas;
using Content.Features.StorageModule.Scripts;
using Core.GlobalSignalsModule.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Content.Features.AIModule.Scripts.Entity.MonoEntity
{
    public class PlayerMonoEntity : MonoEntity<PlayerEntityContext>
    {
        [SerializeField] private GameObject _healingEffect;
        [SerializeField] private float _healingShowTime = 1f;
        private PlayerStorageService _playerStorageService;
        private BasePersistor<PlayerPersistentData> _playerPersistor;
        private IEnumerator _healingCoroutine;
        private IDamageable _damageable;
        private GameFlowStateMachine _gameFlowStateMachine;

        [Inject]
        private void InjectDependencies(PlayerStorageService playerStorageService,
            BasePersistor<PlayerPersistentData> persistor, GameFlowStateMachine gameFlowStateMachine)
        {
            _playerStorageService = playerStorageService;
            _playerPersistor = persistor;
            _gameFlowStateMachine = gameFlowStateMachine;
        }

        protected override void Start()
        {
            base.Start();
            _signalBus.Subscribe<HealPlayerSignal>(OnHealPlayer);
            _signalBus.Fire(new DamagebleCreated(_damageable));
        }

        protected override void InitializeContext()
        {
            base.InitializeContext();
            _damageable = GetComponent<IDamageable>();
            var playerDataModel = _playerPersistor.GetDataModel();
            if (_entityContext.EntityAnimator == null)
                _entityContext.EntityAnimator = GetComponentInChildren<EntityAnimator>();
            _damageable.MaxHealth = playerDataModel.MaxHealth;
            _damageable.CurrentHealth = playerDataModel.CurrentHealth;
            _entityContext.EntityData = _entityDataService.GetEntityData<PlayerEntityData>();
            _entityContext.Storage = _playerStorageService.Storage;
            _entityContext.EntityDamageable = _damageable;
            _entityContext.EntityDamageable.SetHealth(playerDataModel.CurrentHealth);
            _entityContext.EntityDamageable.OnHealthChanged += OnHealthChanged;
            _entityContext.EntityDamageable.OnKilled += OnKilled;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _signalBus.Unsubscribe<HealPlayerSignal>(OnHealPlayer);
            _entityContext.EntityDamageable.OnKilled -= OnKilled;
            _entityContext.EntityDamageable.OnHealthChanged -= OnHealthChanged;
            _signalBus.Fire(new DamageableDestroyed(_damageable));
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

        private void OnKilled()
        {
            var defaultData = _entityDataService.GetEntityData<PlayerEntityData>();
            var currentData = _playerPersistor.GetDataModel();
            _playerPersistor.UpdateModel(new PlayerPersistentData()
            {
                CurrentHealth = defaultData.CurrentHealth,
                MaxHealth = defaultData.MaxHealth,
                Currency = currentData.Currency
            });
            _playerPersistor.SaveData();
            _playerStorageService.Storage.RemoveAllItems();
            _entityContext.NavMeshAgent.ResetPath();
            _gameFlowStateMachine.Enter<EnterSurfaceFlowState>();
        }
    }
}