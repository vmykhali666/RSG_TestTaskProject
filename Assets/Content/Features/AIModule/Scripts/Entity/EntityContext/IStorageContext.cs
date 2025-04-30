using Content.Features.StorageModule.Scripts;

namespace Content.Features.AIModule.Scripts.Entity.EntityContext
{
    public interface IStorageContext
    {
        IStorage Storage { get; set; }
    }
}