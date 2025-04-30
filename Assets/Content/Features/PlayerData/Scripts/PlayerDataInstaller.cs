using Content.Features.PlayerData.Scripts.Datas;
using Core.AssetLoaderModule.Core.Scripts;
using Global.Scripts.Generated;
using Zenject;

namespace Content.Features.PlayerData.Scripts
{
    public class PlayerDataInstaller : Installer<PlayerDataInstaller>
    {
        private IAddressablesAssetLoaderService _addressablesAssetLoaderService;

        public override void InstallBindings()
        {
            _addressablesAssetLoaderService = Container.Resolve<IAddressablesAssetLoaderService>();
            Container.Bind<EntitiesConfiguration>()
                .FromScriptableObject(_addressablesAssetLoaderService.LoadAsset<EntitiesConfiguration>(Address.Configurations.EntitiesConfiguration_Default))
                .AsSingle();
            
            Container.Bind<PlayerTransformModel>()
                .AsSingle();
        }
    }
}