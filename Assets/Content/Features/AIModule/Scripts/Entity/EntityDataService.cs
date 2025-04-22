using Content.Features.PlayerData.Scripts.Datas;

namespace Content.Features.AIModule.Scripts.Entity
{
    public class EntityDataService : IEntityDataService
    {
        private EntitiesConfiguration _entitiesConfiguration;

        public EntityDataService(EntitiesConfiguration entitiesConfiguration) =>
            _entitiesConfiguration = entitiesConfiguration;

        public T GetEntityData<T>() where T : EntityData
        {
            return _entitiesConfiguration.GetDataForEntity<T>();
        }
    }
}