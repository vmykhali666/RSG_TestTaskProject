using System;
using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    [Serializable]
    public class HealPotion : Item, IHealable
    {
        public float HealAmount { get; private set; }

        public HealPotion(float healAmount, string name, Sprite icon, int price, float weight, bool isNewItem = true) :
            base(name, icon, weight, isNewItem)
        {
            HealAmount = healAmount;
        }

        public HealPotion(ItemConfiguration itemConfiguration, float healAmount) : base(itemConfiguration)
        {
            HealAmount = healAmount;
        }
    }
}