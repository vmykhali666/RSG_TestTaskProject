using Content.Features.DamageablesModule.Scripts;

namespace Content.Features.AIModule.Scripts.Entity.EntityContext
{
    public interface IDamageableContext
    {
        IDamageable EntityDamageable { get; set; }
    }
}