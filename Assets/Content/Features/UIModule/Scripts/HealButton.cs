using System.Collections.Generic;
using System.Linq;
using Content.Features.DamageablesModule.Scripts.Signals;
using Content.Features.StorageModule.Scripts;
using Content.Features.StorageModule.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Content.Features.UIModule.Scripts
{
    public class HealButton : MonoBehaviour, IInitializable
    {
        [SerializeField] private Button _button;
        [SerializeField] private Badge _badge;

        private SignalBus _signalBus;
        private IStorage _storage;
        private List<IHealable> _items => _storage.GetAllItems<IHealable>();

        [Inject]
        private void InjectDependencies(SignalBus signalBus, PlayerStorageService playerStorageService)
        {
            _signalBus = signalBus;
            _storage = playerStorageService.Storage;
        }

        public void Initialize()
        {
            UpdateButton();
            _button.onClick.AddListener(OnButtonClicked);
            _signalBus.Subscribe<StorageAddItemSignal>(UpdateButton);
            _signalBus.Subscribe<StorageRemoveItemSignal>(UpdateButton);
            _badge.Initialize();
        }

        private void UpdateButton()
        {
            var itemsCount = _items.Count;
            gameObject.SetActive(itemsCount > 0);
            UpdateButtonBadge(itemsCount);
        }

        private void UpdateButtonBadge(int itemsCount)
        {
            if (itemsCount > 0)
            {
                _badge.Show(itemsCount);
            }
            else
            {
                _badge.Hide();
            }
        }

        private void OnButtonClicked()
        {
            var item = _items.FirstOrDefault();
            if (item != null)
            {
                _signalBus.Fire(new HealPlayerSignal(item));
                _storage.RemoveItem((Item)item);
                UpdateButton();
            }
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            _signalBus.TryUnsubscribe<StorageAddItemSignal>(UpdateButton);
            _signalBus.TryUnsubscribe<StorageRemoveItemSignal>(UpdateButton);
        }
    }
}