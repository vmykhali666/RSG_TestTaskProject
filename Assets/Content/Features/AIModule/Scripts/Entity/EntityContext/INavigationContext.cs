using UnityEngine.AI;

namespace Content.Features.AIModule.Scripts.Entity.EntityContext
{
    public interface INavigationContext
    {
        NavMeshAgent NavMeshAgent { get; set; }
    }
}