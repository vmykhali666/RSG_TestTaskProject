namespace Content.Features.StorageModule.Scripts
{
    public class StorageFactory : IStorageFactory
    {
        public IStorage CreateStorage(IStorage.IStorageSettings settings) =>
            new StandardStorage(settings);
    }
}