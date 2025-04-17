using Content.Features.DamageablesModule.Scripts;

namespace Core.GlobalSignalsModule.Scripts.Signals
{
    public class DamageableDestroyed
    {
        public IDamageable Damageable { get; }

        public DamageableDestroyed(IDamageable damageable)
        {
            Damageable = damageable;
        }
    }
}