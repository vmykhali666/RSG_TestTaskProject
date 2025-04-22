using Content.Features.UIModule.Scripts.HealthBar;
using Content.Features.UIModule.Scripts.Inventory;
using Content.Features.UIModule.Scripts.InventoryToggle;
using Content.Features.UIModule.Scripts.Signals;
using UnityEngine;
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

            Container.BindInterfacesAndSelfTo<InventoryTogglePresenter>().AsSingle();

            Container.BindInterfacesAndSelfTo<InventoryToggleView>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<InventoryPresenter>().AsSingle();

            Container.BindInterfacesAndSelfTo<HealButton>().FromComponentInHierarchy().AsSingle();
        }

        private void BindHealthBar()
        {
            Container.Bind<IHealthBarFactory>().To<HealthBarFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<HealthBarsManager>()
                .AsSingle();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<InventoryToggleSignal>();
        }
    }
}