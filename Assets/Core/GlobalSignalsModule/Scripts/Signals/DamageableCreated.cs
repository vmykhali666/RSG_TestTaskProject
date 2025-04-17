using Content.Features.DamageablesModule.Scripts;

namespace Core.GlobalSignalsModule.Scripts.Signals
{
    public class DamageableCreated
    {
        public IDamageable Damageable { get; }

        public DamageableCreated(IDamageable damageable)
        {
            Damageable = damageable;
        }
    }
}