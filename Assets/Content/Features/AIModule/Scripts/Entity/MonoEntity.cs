using Content.Features.AIModule.Scripts.Entity.EntityBehaviours;
using Content.Features.DamageablesModule.Scripts;
using Content.Features.StorageModule.Scripts;
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

        [Inject]
        public void InjectDependencies(IEntityDataService entityDataService,
            IEntityBehaviourFactory entityBehaviourFactory, IStorageFactory storageFactory)
        {
            _entityBehaviourFactory = entityBehaviourFactory;
            _entityDataService = entityDataService;
            _storageFactory = storageFactory;
        }

        private void Start()
        {
            _entityContext.Entity = this;
            _entityContext.EntityDamageable = GetComponent<IDamageable>();
            _entityContext.EntityData = _entityDataService.GetEntityData(_entityType);
            _entityContext.EntityDamageable.SetHealth(_entityContext.EntityData.StartHealth);
            _entityContext.Storage = CreateStorage();

            SetDefaultBehaviour();
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