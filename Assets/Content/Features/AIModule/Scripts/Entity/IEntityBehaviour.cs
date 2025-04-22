using System;
using Content.Features.AIModule.Scripts.Entity.EntityContext;

namespace Content.Features.AIModule.Scripts.Entity {
    public interface IEntityBehaviour {
        public event Action OnBehaviorEnd;
        public void InitContext(BaseEntityContext entityContext);
        public void Start();
        public void Process();
        public void Stop();
    }
}