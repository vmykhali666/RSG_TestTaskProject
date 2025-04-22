using System.Collections.Generic;
using Zenject;

namespace Content.Features.AIModule.Scripts.Entity.SpawnEntities
{
    public class EntitySpawnService : IInitializable
    {
        private readonly List<EntityMarker> _spawnMarkers;
        private readonly IEntityFactory _entityFactory;

        public EntitySpawnService(List<EntityMarker> spawnMarkers, IEntityFactory entityFactory)
        {
            _spawnMarkers = spawnMarkers;
            _entityFactory = entityFactory;
        }

        public void Initialize()
        {
            foreach (var spawnMarker in _spawnMarkers)
            {
                var entity = _entityFactory.CreateEntity(spawnMarker);
            }
        }
    }
}