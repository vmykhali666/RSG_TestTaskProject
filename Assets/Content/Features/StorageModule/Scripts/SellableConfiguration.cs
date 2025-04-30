using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    [CreateAssetMenu(
        menuName = "Configurations/Inventory/" + nameof(ItemsConfiguration) + "/" + nameof(SellableConfiguration),
        fileName = nameof(SellableConfiguration), order = 0)]
    public class SellableConfiguration : ItemConfiguration, ISellable, IDroppable
    {
        [field: SerializeField] public float Price { get; private set; }

        [field: Range(0, 1)]
        [field: SerializeField]
        public float DropChance { get; private set; }

        public override Item CreateItem()
        {
            return new SellableItem(this, Price, DropChance);
        }
    }
}