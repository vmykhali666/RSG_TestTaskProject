namespace Content.Features.StorageModule.Scripts {
    public interface IStorageFactory {
        public IStorage CreateStorage(IStorage.IStorageSettings storageSettings);
    }
}