using Content.Features.AIModule.Scripts.Entity.EntityBehaviours;
using Content.Features.DamageablesModule.Scripts;
using Content.Features.PlayerData.Scripts;
using Content.Features.StorageModule.Scripts;
using Core.GlobalSignalsModule.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Content.Features.AIModule.Scripts.Entity
{
    public class MonoEntity : MonoBehaviour, IEntity
    {
        [SerializeField] protected EntityContext _entityContext;
        [SerializeField] protected EntityType _entityType;
        [SerializeField] protected bool _isAggressive;
        [SerializeField] private StorageSettings _storageSettings;

        private IEntityBehaviour _currentBehaviour;
        private IEntityDataService _entityDataService;
        private IEntityBehaviourFactory _entityBehaviourFactory;
        private IStorageFactory _storageFactory;
        private SignalBus _signalBus;
        private BasePersistor<PlayerPersistentData> _playerPersistor;

        [Inject]
        public void InjectDependencies(IEntityDataService entityDataService,
            IEntityBehaviourFactory entityBehaviourFactory, IStorageFactory storageFactory, SignalBus signalBus,
            BasePersistor<PlayerPersistentData> persistor)
        {
            _entityBehaviourFactory = entityBehaviourFactory;
            _entityDataService = entityDataService;
            _storageFactory = storageFactory;
            _signalBus = signalBus;
            _playerPersistor = persistor;
        }

        private void Start()
        {
            _entityContext.Entity = this;
            _entityContext.EntityDamageable = GetComponent<IDamageable>();
            _entityContext.EntityData = _entityDataService.GetEntityData(_entityType);
            _entityContext.Storage = CreateStorage();

            PlayerDataPersistance();
            
            _signalBus.Fire(new DamageableCreated(
                _entityContext.EntityDamageable));

            SetDefaultBehaviour();
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

        protected virtual IStorage CreateStorage()
        {
            return _storageFactory.CreateStorage(_storageSettings);
        }

        private void Update() =>
            _currentBehaviour.Process();

        private void OnDestroy()
        {
            if (_currentBehaviour == null)
                return;

            _currentBehaviour.Stop();
            _currentBehaviour.OnBehaviorEnd -= OnBehaviourEnded;
            _entityContext.EntityDamageable.OnHealthChanged -= OnHealthChanged;
            _signalBus.Fire(new DamageableDestroyed(
                _entityContext.EntityDamageable));
        }

        public void SetBehaviour(IEntityBehaviour entityBehaviour)
        {
            if (_currentBehaviour != null)
            {
                _currentBehaviour.Stop();
                _currentBehaviour.OnBehaviorEnd -= OnBehaviourEnded;
            }

            _currentBehaviour = entityBehaviour;
            _currentBehaviour.OnBehaviorEnd += OnBehaviourEnded;
            _currentBehaviour.InitContext(_entityContext);
            _currentBehaviour.Start();
        }

        private void OnBehaviourEnded() =>
            SetDefaultBehaviour();

        private void SetDefaultBehaviour()
        {
            if (_isAggressive)
                SetBehaviour(_entityBehaviourFactory.GetEntityBehaviour<IdleSearchForTargetsEntityBehaviour>());
            else
                SetBehaviour(_entityBehaviourFactory.GetEntityBehaviour<IdleEntityBehaviour>());
        }
    }
}