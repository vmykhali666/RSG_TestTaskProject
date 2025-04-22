namespace Content.Features.AIModule.Scripts.Entity.SpawnEntities
{
    public interface IEntityFactory
    {
        IEntity CreateEntity(EntityMarker spawnMarker);
    }
}