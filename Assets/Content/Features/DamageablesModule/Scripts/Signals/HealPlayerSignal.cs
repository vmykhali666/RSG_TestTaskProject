using Content.Features.StorageModule.Scripts;

namespace Content.Features.DamageablesModule.Scripts.Signals
{
    public class HealPlayerSignal
    {
        public IHealable Item { get; }
        
        public HealPlayerSignal(IHealable item)
        {
            Item = item;
        }
    }
}