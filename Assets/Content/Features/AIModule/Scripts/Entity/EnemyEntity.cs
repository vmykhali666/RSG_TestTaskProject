using Content.Features.AIModule.Scripts.Entity.Datas;

namespace Content.Features.AIModule.Scripts.Entity
{
    public class EnemyEntity : MonoEntity
    {
        protected override EntityData InitializeEntytiData()
        {
            var entityData = _entityDataService.GetEntityData<EnemyEntityData>();
            return entityData;
        }
    }
}