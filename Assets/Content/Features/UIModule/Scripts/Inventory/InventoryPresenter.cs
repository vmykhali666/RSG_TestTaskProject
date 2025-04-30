using System;
using Content.Features.StorageModule.Scripts;
using UnityEngine;
using Zenject;

namespace Content.Features.UIModule.Scripts.Inventory
{
    public class InventoryPresenter : IInitializable, IDisposable
    {
        private readonly InventoryView _inventoryView;
        private readonly IStorage _storage;

        public bool IsOpened { get; private set; }

        public InventoryPresenter(InventoryView inventoryView, PlayerStorageService playerStorageService)
        {
            _inventoryView = inventoryView;
            _storage = playerStorageService.Storage;
        }

        public void Initialize()
        {
            SyncInventoryWithStorage();
            SubscribeToEvents();
        }

        public void OpenInventory(bool isActive)
        {
            if (IsOpened == isActive) return;

            IsOpened = isActive;
            if (IsOpened)
                _inventoryView.Show();
            else
                _inventoryView.Hide();
        }

        private void SyncInventoryWithStorage()
        {
            _inventoryView.SetItems(_storage.GetAllItems());
        }

        private void SubscribeToEvents()
        {
            _storage.OnItemAdded += HandleItemAddedToStorage;
            _storage.OnItemRemoved += HandleItemRemovedFromStorage;
            _inventoryView.OnItemRemoved += HandleItemRemovedFromInventory;
        }

        private void UnsubscribeFromEvents()
        {
            _storage.OnItemAdded -= HandleItemAddedToStorage;
            _storage.OnItemRemoved -= HandleItemRemovedFromStorage;
            _inventoryView.OnItemRemoved -= HandleItemRemovedFromInventory;
        }

        private void HandleItemAddedToStorage(Item item)
        {
            SyncInventoryWithStorage();
        }

        private void HandleItemRemovedFromStorage(Item item)
        {
            SyncInventoryWithStorage();
        }

        private void HandleItemRemovedFromInventory(InventoryItem inventoryItem)
        {
            Debug.Log($"Removed item from inventory: {inventoryItem}");
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}