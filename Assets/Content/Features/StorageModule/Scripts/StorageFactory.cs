using Content.Features.AIModule.Scripts.Entity;
using Zenject;

namespace Content.Features.StorageModule.Scripts
{
    public class StorageFactory : IStorageFactory
    {
        public IStorage CreateStorage(StorageSettings storageSettings) =>
            new StandardStorage(storageSettings);
    }
}