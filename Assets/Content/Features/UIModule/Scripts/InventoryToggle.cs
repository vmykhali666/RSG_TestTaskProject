using System.Linq;
using Content.Features.StorageModule.Scripts;
using Content.Features.StorageModule.Scripts.Signals;
using Content.Features.UIModule.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Content.Features.UIModule.Scripts
{
    public class InventoryToggle : MonoBehaviour, IInitializable
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private InventoryBadge _badge;
        private SignalBus _signalBus;
        private IStorage _storage;
        private InventoryPresenter _inventoryPresenter;

        [Inject]
        private void InjectDependencies(SignalBus signalBus, PlayerStorageService playerStorageService, InventoryPresenter inventoryPresenter)
        {
            _signalBus = signalBus;
            _storage = playerStorageService.Storage;
            _inventoryPresenter = inventoryPresenter;
        }

        public void Initialize()
        {
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
            _toggle.isOn = !_inventoryPresenter.IsOpened;
            _signalBus.Subscribe<StorageAddItemSignal>(OnStorageItemAdded);
            _badge.Initialize();
        }

        private void OnStorageItemAdded()
        {
            var items = _storage.GetAllItems().Count(item => item.IsNewItem);
            if (items > 0)
            {
                _badge.Show(items);
            }
            else
            {
                _badge.Hide();
            }
        }

        private void OnToggleValueChanged(bool isOn)
        {
            _signalBus.Fire(new InventoryToggleSignal(!isOn));
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }
}