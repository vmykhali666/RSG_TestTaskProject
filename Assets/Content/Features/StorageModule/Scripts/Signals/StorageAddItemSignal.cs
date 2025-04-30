namespace Content.Features.StorageModule.Scripts.Signals
{
    public class StorageAddItemSignal
    {
        public Item Item { get; }

        public StorageAddItemSignal(Item item)
        {
            Item = item;
        }
    }
}