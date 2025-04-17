using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    [CreateAssetMenu(
        menuName = "Configurations/Inventory/" + nameof(ItemsConfiguration) + "/" + nameof(HealableConfiguration),
        fileName = nameof(HealableConfiguration), order = 0)]
    public class HealableConfiguration : ItemConfiguration
    {
        public float HealAmount;

        public override Item CreateItem()
        {
            return new HealPotion(this, HealAmount);
        }
    }
}