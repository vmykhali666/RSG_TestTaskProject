using System;
using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    [Serializable]
    public class SellableItem : Item, ISellable, IDroppable
    {
        public float Price { get; set; }

        public SellableItem(float price, float dropChance, string name, Sprite icon, float weight,
            bool isNewItem = true) :
            base(name, icon, weight, isNewItem)
        {
            Price = price;
            DropChance = dropChance;
        }

        public SellableItem(ItemConfiguration itemConfiguration, float price, float dropChance) :
            this(price, dropChance, itemConfiguration.Name, itemConfiguration.Icon, itemConfiguration.Weight,
                itemConfiguration.IsNewItem)
        {
        }

        public float DropChance { get; set; }
    }
}