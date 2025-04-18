using Content.Features.UIModule.Scripts.Signals;
using Zenject;

namespace Content.Features.UIModule.Scripts
{
    public class UIModuleInstaller : Installer<UIModuleInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UIRootReferences>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            DeclareSignals();
            BindInventoryUI();
            BindHealthBar();
        }

        private void BindInventoryUI()
        {
            Container.BindInterfacesAndSelfTo<InventoryView>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<InventoryPresenter>().AsSingle();

            Container.BindInterfacesAndSelfTo<InventoryToggle>().FromComponentInHierarchy().AsSingle();
            
            Container.BindInterfacesAndSelfTo<HealButton>().FromComponentInHierarchy().AsSingle();
        }

        private void BindHealthBar()
        {
            Container.BindInterfacesAndSelfTo<HealthBarInitializer>()
                .AsSingle();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<InventoryToggleSignal>();
        }
    }
}