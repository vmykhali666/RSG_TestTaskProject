using System;
using System.Collections.Generic;
using System.Linq;
using Content.Features.AIModule.Scripts.Entity;

namespace Content.Features.StorageModule.Scripts
{
    public class StandardStorage : IStorage
    {
        private readonly List<Item> _items;
        private readonly float _maxCapacity;

        public event Action<Item> OnItemAdded;
        public event Action<Item> OnItemRemoved;


        public StandardStorage(StorageSettings storageSettings)
        {
            _maxCapacity = storageSettings.MaxCapacity;
            _items = new List<Item>();
        }

        public List<Item> GetAllItems() =>
            _items.ToList();

        public List<T> GetAllItems<T>()
        {
            return _items.OfType<T>().ToList();
        }

        public void AddItem(Item item)
        {
            if (_items.Contains(item))
                return;

            _items.Add(item);
            OnItemAdded?.Invoke(item);
        }

        public void AddItems(List<Item> items)
        {
            items.ForEach(_items.Add);
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
            items.ForEach(RemoveItem);
        }

        public void RemoveAllItems()
        {
            for (var i = _items.Count - 1; i >= 0; i--)
            {
                RemoveItem(_items[i]);
            }
        }

        public float GetCurrentCapacity()
        {
            return _items.Sum(item => item.Weight);
        }

        public float GetMaxCapacity()
        {
            return _maxCapacity;
        }
    }
}