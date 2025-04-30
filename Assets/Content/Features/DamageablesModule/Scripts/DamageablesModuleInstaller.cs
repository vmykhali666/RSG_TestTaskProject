using Content.Features.DamageablesModule.Scripts.Signals;
using Content.Features.InteractionModule;
using Core.AssetLoaderModule.Core.Scripts;
using Global.Scripts.Generated;
using Zenject;

namespace Content.Features.DamageablesModule.Scripts
{
    public class DamageablesModuleInstaller : Installer<DamageablesModuleInstaller>
    {
        public override void InstallBindings()
        {
            BindSignals();
        }

        private void BindSignals()
        {
            Container.DeclareSignal<HealPlayerSignal>().OptionalSubscriber();
        }
    }
}