using Content.Features.AIModule.Scripts.Entity;
using Content.Features.AIModule.Scripts.Entity.EntityBehaviours;
using Content.Features.AIModule.Scripts.Entity.MonoEntity;
using Content.Features.DamageablesModule.Scripts;
using UnityEngine;
using Zenject;

namespace Content.Features.InteractionModule {
    public class AttackInteractable : MonoBehaviour, IInteractable {
        private IEntityBehaviourFactory _entityBehaviourFactory;
        
        [Inject]
        public void InjectDependencies(IEntityBehaviourFactory entityBehaviourFactory) =>
            _entityBehaviourFactory = entityBehaviourFactory;

        public void Interact(IEntity entity) {
            var targetDamageable = GetComponent<IDamageable>();
            //TODO: player can`t attack itself
            if (targetDamageable.DamageableType == DamageableType.Player && entity is PlayerMonoEntity) {
                Debug.Log("Player cannot attack itself");
                return;
            }
            var attackEntityBehaviour = _entityBehaviourFactory.GetEntityBehaviour<AttackEntityBehaviour>();
            attackEntityBehaviour.SetTarget(targetDamageable);
            entity.SetBehaviour(attackEntityBehaviour);
        }
    }
}