namespace Content.Features.PlayerData.Scripts.Datas
{
    public interface IEntityDataService
    {
        public T GetEntityData<T>() where T : EntityData;
    }
}