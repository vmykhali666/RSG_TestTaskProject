namespace Content.Features.UIModule.Scripts.Signals
{
    public class InventoryToggleSignal
    {
        public bool IsActive { get; }

        public InventoryToggleSignal(bool isActive)
        {
            IsActive = isActive;
        }
    }
}