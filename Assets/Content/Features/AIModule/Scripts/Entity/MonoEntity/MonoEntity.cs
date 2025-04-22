using Content.Features.AIModule.Scripts.Entity.EntityBehaviours;
using Content.Features.AIModule.Scripts.Entity.EntityContext;
using Content.Features.PlayerData.Scripts.Datas;
using UnityEngine;
using Zenject;

namespace Content.Features.AIModule.Scripts.Entity.MonoEntity
{
    public abstract class MonoEntity<T> : MonoBehaviour, IEntity where T : BaseEntityContext
    {
        [SerializeField] protected T _entityContext;

        private IEntityBehaviour _currentBehaviour;
        protected IEntityDataService _entityDataService;
        protected IEntityBehaviourFactory _entityBehaviourFactory;
        protected SignalBus _signalBus;

        [Inject]
        public void InjectDependencies(IEntityDataService entityDataService,
            IEntityBehaviourFactory entityBehaviourFactory, SignalBus signalBus)
        {
            _entityBehaviourFactory = entityBehaviourFactory;
            _entityDataService = entityDataService;
            _signalBus = signalBus;
        }

        protected virtual void Start()
        {
            InitializeContext();
        }

        protected virtual void InitializeContext()
        {
            _entityContext.Entity = this;
        }

        private void Update() =>
            _currentBehaviour?.Process();

        protected virtual void OnDestroy()
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

        protected virtual void OnBehaviourEnded() =>
            SetDefaultBehaviour();

        protected virtual void SetDefaultBehaviour()
        {
            SetBehaviour(_entityBehaviourFactory.GetEntityBehaviour<IdleEntityBehaviour>());
        }
    }
}