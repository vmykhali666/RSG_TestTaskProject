using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    public class StandardStorage : IStorage
    {
        private readonly List<Item> _items = new();
        private float _maxCapacity;

        public event Action<Item> OnItemAdded;
        public event Action<Item> OnItemRemoved;

        public StandardStorage(IStorage.IStorageSettings storageSettings)
        {
            _maxCapacity = storageSettings.MaxCapacity;
        }

        public List<Item> GetAllItems() =>
            _items.ToList();

        public void AddItem(Item item)
        {
            if (_items.Contains(item))
                return;

            _items.Add(item);
            OnItemAdded?.Invoke(item);
        }

        public void AddItems(List<Item> items)
        {
            foreach (Item item in items)
                AddItem(item);
        }

        public void RemoveItem(Item item)
        {
            if (_items.Contains(item) is false)
                return;

            _items.Remove(item);
            OnItemRemoved?.Invoke(item);
        }

        public void RemoveItems(List<Item> items)
        {
            foreach (Item item in items)
                RemoveItem(item);
        }

        public void RemoveAllItems()
        {
            foreach (Item item in _items)
                RemoveItem(item);
        }

        public float GetCurrentCapacity()
        {
            return _items.Sum(item => item.Weight);
        }

        public float GetMaxCapacity()
        {
            return _maxCapacity;
        }

        [Serializable]
        public class StorageSettings : IStorage.IStorageSettings
        {
            [Range(0, 100)]
            [SerializeField] private float _maxCapacity = 100f;
            public float MaxCapacity => _maxCapacity;
        }
    }
}