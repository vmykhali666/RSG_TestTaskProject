using System.Collections.Generic;
using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    [CreateAssetMenu(
        menuName = "Configurations/Inventory/" + nameof(ItemsConfiguration) + "/" + nameof(ItemsConfiguration),
        fileName = nameof(ItemsConfiguration) + "_Default", order = 0)]
    public class ItemsConfiguration : ScriptableObject
    {
        [SerializeField] private List<ItemConfiguration> _itemConfigurations;
        [SerializeField] private ItemConfiguration _defaultItem;

        private Dictionary<ItemType, ItemConfiguration> _configurationMap;

        private void OnEnable()
        {
            _configurationMap = new Dictionary<ItemType, ItemConfiguration>();
            foreach (var itemConfiguration in _itemConfigurations)
            {
                if (!_configurationMap.ContainsKey(itemConfiguration.ItemType))
                {
                    _configurationMap.Add(itemConfiguration.ItemType, itemConfiguration);
                }
                else
                {
                    Debug.LogWarning(
                        $"Duplicate item configuration found for {itemConfiguration.ItemType}. Using the first one.");
                }
            }
        }

        public ItemConfiguration GetItemConfiguration(ItemType itemType)
        {
            if (_configurationMap.TryGetValue(itemType, out var itemConfiguration))
            {
                return itemConfiguration;
            }

            Debug.LogWarning($"Item configuration for {itemType} not found. Using default item configuration.");
            return _defaultItem;
        }
    }
}