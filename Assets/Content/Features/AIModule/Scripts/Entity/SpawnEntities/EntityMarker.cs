using UnityEngine;

namespace Content.Features.AIModule.Scripts.Entity.SpawnEntities
{
    public class EntityMarker : MonoBehaviour
    {
        public EntityType SpawnType;
        public Transform SpawnPoint => transform;

        public void OnDrawGizmos()
        {
            Gizmos.color = SpawnType == EntityType.Player ? Color.blue : Color.red;
            Gizmos.DrawSphere(SpawnPoint.position, 0.5f);
        }
    }
}