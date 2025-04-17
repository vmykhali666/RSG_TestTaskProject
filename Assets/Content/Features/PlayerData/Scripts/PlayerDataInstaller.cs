using Zenject;

namespace Content.Features.PlayerData.Scripts
{
    public class PlayerDataInstaller : Installer<PlayerDataInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerTransformModel>()
                .AsSingle();

            BindDataPersistors();
        }

        private void BindDataPersistors()
        {
            BindPlayerPersistor();
        }

        private void BindPlayerPersistor()
        {
            // explicitly bind Interfaces to the PlayerPersistentData to work IInitializable and IDisposable
            Container.BindInterfacesAndSelfTo<PlayerPrefsPersistor<PlayerPersistentData>>()
                .AsSingle()
                .WithArguments("PlayerPersistentData");
            
            Container.Bind<BasePersistor<PlayerPersistentData>>()
                .To<PlayerPrefsPersistor<PlayerPersistentData>>()
                .FromResolve();
        }
    }
}