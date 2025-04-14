using Core.AssetLoaderModule.Core.Scripts;
using Global.Scripts.Generated;
using Zenject;

namespace Content.Features.StorageModule.Scripts
{
    public class StorageModuleInstaller : Installer<StorageModuleInstaller>
    {
        public override void InstallBindings()
        {
            IAddressablesAssetLoaderService addressablesAssetLoaderService =
                Container.Resolve<IAddressablesAssetLoaderService>();
            Container.Bind<ItemsConfiguration>()
                .FromScriptableObject(
                    addressablesAssetLoaderService.LoadAsset<ItemsConfiguration>(Address.Configurations
                        .ItemsConfiguration_Default))
                .AsSingle();

            Container.Bind<IItemFactory>()
                .To<ItemFactory>()
                .AsSingle();

            Container.Bind<IStorageFactory>()
                .To<StorageFactory>()
                .AsSingle();

            Container.Bind<IStorageConstraintService>()
                .To<StorageConstraintService>()
                .AsSingle();

            BindStorageConstraints();
        }

        private void BindStorageConstraints()
        {
            Container.Bind<IStorageConstraint>()
                .To<CapacityStorageConstraint>()
                .AsTransient();
        }
    }
}