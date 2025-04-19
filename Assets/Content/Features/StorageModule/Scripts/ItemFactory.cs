using System.Linq;
using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    public class ItemFactory : IItemFactory
    {
        private readonly ItemsConfiguration _itemsConfiguration;

        public ItemFactory(ItemsConfiguration itemsConfiguration) =>
            _itemsConfiguration = itemsConfiguration;

        public Item GetItem(ItemType itemType)
        {
            var itemConfiguration = _itemsConfiguration.GetItemConfiguration(itemType);
            return itemConfiguration.CreateItem();
        }

        public T GetItem<TInterface, T>() where T : Item
        {
            var configs = _itemsConfiguration.GetItemConfiguration<TInterface>();

            return configs.Select(config => config.CreateItem())
                .OfType<T>().FirstOrDefault();
        }
    }
}