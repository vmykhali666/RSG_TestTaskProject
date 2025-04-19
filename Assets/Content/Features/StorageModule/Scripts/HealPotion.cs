using System;
using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    [Serializable]
    public class HealPotion : Item, IHealable, IDroppable, IBuyable
    {
        public float HealAmount { get; set; }

        public HealPotion(float healAmount, float dropChance, float buyPrice, string name, Sprite icon, float weight,
            bool isNewItem = true) :
            base(name, icon, weight, isNewItem)
        {
            HealAmount = healAmount;
            DropChance = dropChance;
            BuyPrice = buyPrice;
        }

        public HealPotion(ItemConfiguration itemConfiguration, float healAmount, float dropChance, float buyPrice) :
            this(healAmount, dropChance, buyPrice, itemConfiguration.Name, itemConfiguration.Icon, itemConfiguration.Weight,
                itemConfiguration.IsNewItem)
        {
        }

        public float DropChance { get; set; }
        public float BuyPrice { get; }
    }
}