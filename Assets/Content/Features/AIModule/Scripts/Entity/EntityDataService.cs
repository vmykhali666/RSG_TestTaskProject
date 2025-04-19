using Content.Features.AIModule.Scripts.Entity.Datas;

namespace Content.Features.AIModule.Scripts.Entity
{
    public class EntityDataService : IEntityDataService
    {
        private EntitiesConfiguration _entitiesConfiguration;

        public EntityDataService(EntitiesConfiguration entitiesConfiguration) =>
            _entitiesConfiguration = entitiesConfiguration;

        public EntityData GetEntityData<T>() where T : EntityData
        {
            return _entitiesConfiguration.GetDataForEntity<T>();
        }
    }
}