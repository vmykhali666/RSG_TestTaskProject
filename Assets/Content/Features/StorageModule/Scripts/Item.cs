using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    public abstract class Item
    {
        public string Name { get; private set; }
        public Sprite Icon { get; private set; }

        public float Weight { get; private set; }
        public bool IsNewItem { get; private set; }

        public Item(string name, Sprite icon, float weight, bool isNewItem = true)
        {
            Name = name;
            Icon = icon;
            Weight = weight;
            IsNewItem = isNewItem;
        }

        public Item(ItemConfiguration itemConfiguration)
        {
            Name = itemConfiguration.Name;
            Icon = itemConfiguration.Icon;
            Weight = itemConfiguration.Weight;
            IsNewItem = itemConfiguration.IsNewItem;
        }
    }
}