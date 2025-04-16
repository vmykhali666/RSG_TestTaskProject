using Content.Features.UIModule.Scripts.Signals;
using Zenject;

namespace Content.Features.UIModule.Scripts
{
    public class UIModuleInstaller : Installer<UIModuleInstaller>
    {
        public override void InstallBindings()
        {
            DeclareSignals();
            BindUI();
        }

        private void BindUI()
        {
            Container.BindInterfacesAndSelfTo<InventoryView>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<InventoryPresenter>().AsSingle();

            Container.BindInterfacesTo<InventoryToggle>().FromComponentInHierarchy().AsSingle();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<InventoryToggleSignal>();
        }
    }
}