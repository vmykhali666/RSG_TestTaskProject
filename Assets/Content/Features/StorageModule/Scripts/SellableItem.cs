using System;
using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    [Serializable]
    public class SellableItem : Item, ISellable
    {
        public float Price { get; private set; }

        public SellableItem(float price, string name, Sprite icon, float weight, bool isNewItem = true) :
            base(name, icon, weight, isNewItem)
        {
            Price = price;
        }

        public SellableItem(ItemConfiguration itemConfiguration, float price) : base(itemConfiguration)
        {
            Price = price;
        }
    }
}