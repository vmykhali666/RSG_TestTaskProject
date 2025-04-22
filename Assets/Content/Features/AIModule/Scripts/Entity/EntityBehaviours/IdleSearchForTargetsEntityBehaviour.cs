using System;
using Content.Features.AIModule.Scripts.Entity.EntityContext;
using Content.Features.DamageablesModule.Scripts;

namespace Content.Features.AIModule.Scripts.Entity.EntityBehaviours
{
    public class IdleSearchForTargetsEntityBehaviour : IEntityBehaviour
    {
        private BaseEntityContext _entityContext;

        public event Action OnBehaviorEnd;

        public void InitContext(BaseEntityContext entityContext) =>
            _entityContext = entityContext;

        public void Start()
        {
            _entityContext.EntityAnimator.SetIsAttacking(false);
            if (_entityContext is INavigationContext navContext)
            {
                navContext.NavMeshAgent.ResetPath();
            }
        }

        public void Process()
        {
            if (_entityContext is ITargetsFinderContext targetsFinderContext &&
                targetsFinderContext.TargetsFinder.TryFindDamageable(out IDamageable damageable))
            {
                damageable.Interactable.Interact(_entityContext.Entity);
            }

            OnBehaviorEnd?.Invoke();
        }

        public void Stop()
        {
        }
    }
}