using System;
using Zenject;

namespace Content.Features.StorageModule.Scripts
{
    /// <summary>
    /// Storage service for player data.
    /// Saves players inventory between scenes.
    /// </summary>
    public class PlayerStorageService : IInitializable, IDisposable
    {
        public IStorage Storage { get; private set; }
        private readonly IStorageFactory _storageFactory;
        private readonly StorageSettings _storageSettings;

        public PlayerStorageService(IStorageFactory storageFactory, StorageSettings storageSettings)
        {
            _storageFactory = storageFactory;
            _storageSettings = storageSettings;
        }
        
        public void Initialize()
        {
            Storage = _storageFactory.CreateStorage(_storageSettings);
        }
        
        public void Dispose()
        {
            Storage.RemoveAllItems();
            Storage = null;
        }
    }
}