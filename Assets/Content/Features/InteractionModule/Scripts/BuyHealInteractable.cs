using Content.Features.AIModule.Scripts.Entity;
using Content.Features.AIModule.Scripts.Entity.EntityBehaviours;
using UnityEngine;
using Zenject;

namespace Content.Features.InteractionModule
{
    public class BuyHealInteractable : MonoBehaviour, IInteractable {
        private IEntityBehaviourFactory _entityBehaviourFactory;
        
        [Inject]
        public void InjectDependencies(IEntityBehaviourFactory entityBehaviourFactory) =>
            _entityBehaviourFactory = entityBehaviourFactory;

        public void Interact(IEntity entity) {
            var buyHealEntityBehaviour = _entityBehaviourFactory.GetEntityBehaviour<BuyHealEntityBehaviour>();
            buyHealEntityBehaviour.SetTarget(transform);
            entity.SetBehaviour(buyHealEntityBehaviour);
        }
    }
}