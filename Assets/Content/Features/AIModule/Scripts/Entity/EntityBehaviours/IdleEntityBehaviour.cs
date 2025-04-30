using System;
using Content.Features.AIModule.Scripts.Entity.EntityContext;

namespace Content.Features.AIModule.Scripts.Entity.EntityBehaviours
{
    public class IdleEntityBehaviour : IEntityBehaviour
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
        }

        public void Stop()
        {
        }
    }
}