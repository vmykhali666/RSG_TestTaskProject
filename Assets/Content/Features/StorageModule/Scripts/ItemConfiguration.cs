using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    public abstract class ItemConfiguration : ScriptableObject
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _weight;
        [SerializeField] private bool _isNewItem;

        public ItemType ItemType => _itemType;
        public string Name => _name;
        public Sprite Icon => _icon;
        public float Weight => _weight;
        public bool IsNewItem => _isNewItem;

        public abstract Item CreateItem();
    }
}