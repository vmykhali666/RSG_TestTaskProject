namespace Content.Features.AIModule.Scripts.Entity.Datas
{
    public interface IEntityDataService
    {
        public EntityData GetEntityData<T>() where T : EntityData;
    }
}