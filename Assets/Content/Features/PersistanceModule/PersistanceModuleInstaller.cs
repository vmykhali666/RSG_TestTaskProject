using Content.Features.PlayerData.Scripts;
using Content.Features.PlayerData.Scripts.Datas;
using Zenject;

namespace Content.Features.PersistanceModule
{
    public class PersistanceModuleInstaller : Installer<PersistanceModuleInstaller>
    {
        private EntitiesConfiguration _entitiesConfiguration;

        public override void InstallBindings()
        {
            _entitiesConfiguration =
                Container.Resolve<EntitiesConfiguration>();

            BindDataPersistors();
        }

        private void BindDataPersistors()
        {
            BindPlayerPersistor();
        }

        private void BindPlayerPersistor()
        {
            var playerEntityData = _entitiesConfiguration.GetDataForEntity<PlayerEntityData>();
            if (playerEntityData != null)
            {
                var persistDataDefault = (PlayerPersistentData)playerEntityData;

                Container.Bind<PlayerPersistentData>()
                    .FromInstance(persistDataDefault)
                    .AsSingle();

                // explicitly bind Interfaces to the PlayerPersistentData to work IInitializable and IDisposable
                Container.BindInterfacesAndSelfTo<PlayerPrefsPersistor<PlayerPersistentData>>()
                    .AsSingle()
                    .WithArguments("PlayerPersistentData", persistDataDefault);

                Container.Bind<BasePersistor<PlayerPersistentData>>().To<PlayerPrefsPersistor<PlayerPersistentData>>()
                    .FromResolve();
            }
            else
            {
                throw new System.Exception(
                    $"Can`t convert {typeof(PlayerEntityData)} to {typeof(PlayerPersistentData)}" +
                    $"\n" + $"Realize implicit conversion in the {typeof(PlayerEntityData)} class");
            }
        }
    }
}