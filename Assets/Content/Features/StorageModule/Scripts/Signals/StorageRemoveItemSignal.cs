namespace Content.Features.StorageModule.Scripts.Signals
{
    public class StorageRemoveItemSignal
    {
        public Item Item { get; }

        public StorageRemoveItemSignal(Item item)
        {
            Item = item;
        }
    }
}