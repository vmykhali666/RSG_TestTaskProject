using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    [CreateAssetMenu(
        menuName = "Configurations/Inventory/" + nameof(ItemsConfiguration) + "/" + nameof(ItemConfiguration),
        fileName = nameof(ItemConfiguration), order = 0)]
    public class ItemConfiguration : ScriptableObject
    {
        public ItemType ItemType;
        public string Name;
        public Sprite Icon;
        [Range(0, 10000)] public int Price;
        [Range(0, 100)] public float Weight;
        public bool IsNewItem = true;

        public virtual Item CreateItem()
        {
            return new Item(this);
        }
    }
}