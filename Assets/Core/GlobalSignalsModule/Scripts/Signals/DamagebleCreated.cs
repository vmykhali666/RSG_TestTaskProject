using Content.Features.DamageablesModule.Scripts;

namespace Core.GlobalSignalsModule.Scripts.Signals
{
    public class DamagebleCreated
    {
        public IDamageable Damageable { get; }

        public DamagebleCreated(IDamageable damageable)
        {
            Damageable = damageable;
        }
    }
}