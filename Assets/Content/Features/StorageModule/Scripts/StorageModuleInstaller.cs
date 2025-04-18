using Content.Features.DamageablesModule.Scripts;
using Content.Features.DamageablesModule.Scripts.Signals;
using Content.Features.StorageModule.Scripts.Constraints;
using Content.Features.StorageModule.Scripts.Signals;
using Core.AssetLoaderModule.Core.Scripts;
using Global.Scripts.Generated;
using Zenject;

namespace Content.Features.StorageModule.Scripts
{
    public class StorageModuleInstaller : Installer<StorageModuleInstaller>
    {
        private IAddressablesAssetLoaderService _assetLoaderService;

        public override void InstallBindings()
        {
            _assetLoaderService =
                Container.Resolve<IAddressablesAssetLoaderService>();

            BindSignals();

            BindConfigs(_assetLoaderService);

            BindFactories();

            BindStorageConstraints();

            BindServices();
        }

        private void BindSignals()
        {
            Container.DeclareSignal<StorageAddItemSignal>();

            Container.DeclareSignal<StorageRemoveItemSignal>();
        }

        private void BindServices()
        {
            Container.Bind<IStorageConstraintService>()
                .To<StorageConstraintService>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerStorageService>()
                .AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.Bind<IItemFactory>()
                .To<ItemFactory>()
                .AsSingle();

            Container.Bind<IStorageFactory>()
                .To<StorageFactory>()
                .AsSingle();
        }

        private void BindConfigs(IAddressablesAssetLoaderService addressablesAssetLoaderService)
        {
            Container.Bind<ItemsConfiguration>()
                .FromScriptableObject(
                    addressablesAssetLoaderService.LoadAsset<ItemsConfiguration>(Address.Configurations
                        .ItemsConfiguration_Default))
                .AsSingle();

            Container.Bind<StorageSettings>()
                .FromScriptableObject(
                    addressablesAssetLoaderService.LoadAsset<StorageSettings>(Address.Configurations
                        .StorageSettings_Default))
                .AsSingle();
        }

        private void BindStorageConstraints()
        {
            Container.Bind<IStorageConstraint>()
                .To<CapacityStorageConstraint>()
                .AsTransient();
        }
    }
}