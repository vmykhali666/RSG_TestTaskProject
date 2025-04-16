using System;
using Content.Features.StorageModule.Scripts.Signals;
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
        private readonly SignalBus _signalBus;

        public PlayerStorageService(IStorageFactory storageFactory, StorageSettings storageSettings,
            SignalBus signalBus)
        {
            _storageFactory = storageFactory;
            _storageSettings = storageSettings;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            Storage = _storageFactory.CreateStorage(_storageSettings);
            Storage.OnItemAdded += OnItemAdded;
            Storage.OnItemRemoved += OnItemRemoved;
        }

        private void OnItemRemoved(Item item)
        {
            _signalBus.Fire(new StorageRemoveItemSignal(item));
        }

        private void OnItemAdded(Item item)
        {
            _signalBus.Fire(new StorageAddItemSignal(item));
        }

        public void Dispose()
        {
            Storage.OnItemAdded -= OnItemAdded;
            Storage.OnItemRemoved -= OnItemRemoved;
            Storage.RemoveAllItems();
            Storage = null;
        }
    }
}