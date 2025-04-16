using System;
using Content.Features.StorageModule.Scripts;
using Content.Features.UIModule.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Content.Features.UIModule.Scripts
{
    public class InventoryPresenter : IInitializable, IDisposable
    {
        private readonly InventoryView _inventoryView;
        private readonly PlayerStorageService _playerStorageService;
        private readonly IStorage _storage;
        private readonly SignalBus _signalBus;
        private bool _isOpened;
        public bool IsOpened
        {
            get => _isOpened;
            private set
            {
                _isOpened = value;
                if (_isOpened)
                {
                    _inventoryView.Show();
                }
                else
                {
                    _inventoryView.Hide();
                }
            }
        }

        public InventoryPresenter(InventoryView inventoryView, PlayerStorageService playerStorageService, SignalBus signalBus)
        {
            _inventoryView = inventoryView;
            _playerStorageService = playerStorageService;
            _storage = playerStorageService.Storage;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _inventoryView.SetItems(_storage.GetAllItems());
            _storage.OnItemAdded += OnItemAddedToStorage;
            _storage.OnItemRemoved += OnItemRemovedFromStorage;
            _inventoryView.OnItemRemoved += OnItemRemovedFromInventory;
            _signalBus.Subscribe<InventoryToggleSignal>(OnInventoryToggle);
            
        }

        private void OnInventoryToggle(InventoryToggleSignal obj)
        {
            IsOpened = obj.IsActive;
        }


        private void OnItemRemovedFromInventory(InventoryItem obj)
        {
            Debug.Log($"Removed item from inventory {obj}");
        }

        private void OnItemAddedToStorage(Item item)
        {
            _inventoryView.SetItems(_storage.GetAllItems());
        }

        private void OnItemRemovedFromStorage(Item item)
        {
            _inventoryView.SetItems(_storage.GetAllItems());
        }

        public void Dispose()
        {
            _storage.OnItemAdded -= OnItemAddedToStorage;
            _storage.OnItemRemoved -= OnItemRemovedFromStorage;
            _inventoryView.OnItemRemoved -= OnItemRemovedFromInventory;
            _signalBus.Unsubscribe<InventoryToggleSignal>(OnInventoryToggle);
        }
    }
}