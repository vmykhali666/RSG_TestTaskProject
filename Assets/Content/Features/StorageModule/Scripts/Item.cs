using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    public class Item
    {
        public string Name { get; private set; }
        public Sprite Icon { get; private set; }
        public int Price { get; private set; }
        public float Weight { get; private set; }

        public bool IsNewItem { get; private set; }

        public Item(string name, Sprite icon, int price, float weight, bool isNewItem = true)
        {
            Name = name;
            Icon = icon;
            Price = price;
            Weight = weight;
            IsNewItem = isNewItem;
        }

        public Item(ItemConfiguration itemConfiguration)
        {
            Name = itemConfiguration.Name;
            Icon = itemConfiguration.Icon;
            Price = itemConfiguration.Price;
            Weight = itemConfiguration.Weight;
            IsNewItem = itemConfiguration.IsNewItem;
        }
    }
}