using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    public abstract class Item
    {
        public string Name { get; set; }
        public Sprite Icon { get; set; }

        public float Weight { get; set; }
        public bool IsNewItem { get; set; }

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