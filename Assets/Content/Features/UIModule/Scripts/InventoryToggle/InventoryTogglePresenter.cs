using System;
using System.Linq;
using Content.Features.StorageModule.Scripts;
using Content.Features.UIModule.Scripts.Inventory;
using Content.Features.UIModule.Scripts.Signals;
using Zenject;

namespace Content.Features.UIModule.Scripts.InventoryToggle
{
    public class InventoryTogglePresenter : IInitializable, IDisposable
    {
        private readonly InventoryToggleView _view;
        private readonly SignalBus _signalBus;
        private readonly IStorage _storage;
        private readonly InventoryPresenter _inventoryPresenter;

        [Inject]
        public InventoryTogglePresenter(
            InventoryToggleView view,
            SignalBus signalBus,
            PlayerStorageService playerStorageService,
            InventoryPresenter inventoryPresenter)
        {
            _view = view;
            _signalBus = signalBus;
            _storage = playerStorageService.Storage;
            _inventoryPresenter = inventoryPresenter;
        }

        public void Initialize()
        {
            _view.Initialize();
            _view.SetToggleState(!_inventoryPresenter.IsOpened);
            _view.OnToggleChanged += OnToggleValueChanged;
            _storage.OnItemAdded += UpdateStorageItemBadge;
            _storage.OnItemRemoved += UpdateStorageItemBadge;

            UpdateStorageItemBadge();
        }

        private void UpdateStorageItemBadge(Item item = null)
        {
            var items = _storage.GetAllItems().Count(i => i.IsNewItem);
            if (items > 0)
            {
                _view.ShowBadge(items);
            }
            else
            {
                _view.HideBadge();
            }
        }

        private void OnToggleValueChanged(bool isOn)
        {
            _inventoryPresenter.OpenInventory(!isOn);
            _signalBus.Fire(new InventoryToggleSignal(!isOn));
        }

        public void Dispose()
        {
            _view.OnToggleChanged -= OnToggleValueChanged;
            _storage.OnItemAdded -= UpdateStorageItemBadge;
            _storage.OnItemRemoved -= UpdateStorageItemBadge;
        }
    }
}