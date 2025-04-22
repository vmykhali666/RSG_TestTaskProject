using Content.Features.AIModule.Scripts.Entity.SpawnEntities;
using Content.Features.PrefabSpawner;
using UnityEngine;
using Zenject;

namespace Content.Features.GameBootstrapModule.Scripts.ProjectDI
{
    [CreateAssetMenu(menuName = "Configurations/GameBootstrap/" + nameof(SurfaceSceneContextInstaller),
        fileName = nameof(SurfaceSceneContextInstaller) + "_Default", order = 0)]
    public class SurfaceSceneContextInstaller : ScriptableObjectInstaller<SurfaceSceneContextInstaller>
    {
        public override void InstallBindings()
        {
            PrefabSpawnerInstaller.Install(Container);
            BindEntitiesInScene();
        }

        private void BindEntitiesInScene()
        {
            Container.Bind<IEntityFactory>().To<EntityFactory>().AsSingle();

            Container.Bind<EntityMarker>().FromComponentsInHierarchy()
                .AsTransient()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<EntitySpawnService>().AsSingle();
        }
    }
}