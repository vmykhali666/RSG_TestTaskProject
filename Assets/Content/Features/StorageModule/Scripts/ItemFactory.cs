namespace Content.Features.StorageModule.Scripts {
    public class ItemFactory : IItemFactory {
        private readonly ItemsConfiguration _itemsConfiguration;

        public ItemFactory(ItemsConfiguration itemsConfiguration) =>
            _itemsConfiguration = itemsConfiguration;

        public Item GetItem(ItemType itemType)
        {
            var itemConfiguration = _itemsConfiguration.GetItemConfiguration(itemType);
            return itemConfiguration.CreateItem();
        }
    }
}