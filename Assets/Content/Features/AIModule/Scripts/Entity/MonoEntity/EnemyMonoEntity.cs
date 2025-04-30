using Content.Features.AIModule.Scripts.Entity.EntityBehaviours;
using Content.Features.AIModule.Scripts.Entity.EntityContext;
using Content.Features.DamageablesModule.Scripts;
using Content.Features.EntityAnimatorModule.Scripts;
using Content.Features.PlayerData.Scripts.Datas;
using Core.GlobalSignalsModule.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Content.Features.AIModule.Scripts.Entity.MonoEntity
{
    public class EnemyMonoEntity : MonoEntity<EnemyEntityContext>
    {
        private IDamageable _damageable;
        public bool IsAggressive { get; private set; }

        [Inject]
        private void InjectDependencies(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        protected override void Start()
        {
            base.Start();
            SetDefaultBehaviour();
            _signalBus.Fire(new DamagebleCreated(_damageable));
        }

        protected override void InitializeContext()
        {
            base.InitializeContext();
            _damageable = GetComponent<IDamageable>();
            var enemyData = _entityDataService.GetEntityData<EnemyEntityData>();
            IsAggressive = enemyData?.IsAggressive ?? false;
            if (_entityContext.EntityAnimator == null)
                _entityContext.EntityAnimator = GetComponentInChildren<EntityAnimator>();
            if (enemyData == null) return;
            _entityContext.EntityData = enemyData;
            _damageable.MaxHealth = enemyData.MaxHealth;
            _damageable.CurrentHealth = enemyData.CurrentHealth;
            _entityContext.EntityDamageable = _damageable;
            _entityContext.EntityDamageable.SetHealth(enemyData.CurrentHealth);
        }

        protected override void SetDefaultBehaviour()
        {
            if (IsAggressive)
                SetBehaviour(_entityBehaviourFactory.GetEntityBehaviour<IdleSearchForTargetsEntityBehaviour>());
            else
            {
                base.SetDefaultBehaviour();
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _signalBus.Fire(new DamageableDestroyed(_damageable));
        }
    }
}