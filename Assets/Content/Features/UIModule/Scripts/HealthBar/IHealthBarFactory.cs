using Content.Features.DamageablesModule.Scripts;

namespace Content.Features.UIModule.Scripts.HealthBar
{
    public interface IHealthBarFactory
    {
        HealthBarPresenter Create(IDamageable damageable);
    }
}