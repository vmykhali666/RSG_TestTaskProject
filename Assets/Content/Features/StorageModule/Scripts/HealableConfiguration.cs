using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    [CreateAssetMenu(
        menuName = "Configurations/Inventory/" + nameof(ItemsConfiguration) + "/" + nameof(HealableConfiguration),
        fileName = nameof(HealableConfiguration), order = 0)]
    public class HealableConfiguration : ItemConfiguration, IHealable, IDroppable
    {
        [field: SerializeField] public float HealAmount { get; private set; }

        [field: Range(0, 100)]
        [field: SerializeField]
        public float DropChance { get; private set; }

        public override Item CreateItem()
        {
            return new HealPotion(this, HealAmount);
        }
    }
}