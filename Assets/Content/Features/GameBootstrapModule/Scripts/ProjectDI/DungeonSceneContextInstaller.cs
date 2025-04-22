using Content.Features.AIModule.Scripts.Entity.SpawnEntities;
using Content.Features.PrefabSpawner;
using UnityEngine;
using Zenject;

namespace Content.Features.GameBootstrapModule.Scripts.ProjectDI
{
    [CreateAssetMenu(menuName = "Configurations/GameBootstrap/" + nameof(DungeonSceneContextInstaller),
        fileName = nameof(DungeonSceneContextInstaller) + "_Default", order = 0)]
    public class DungeonSceneContextInstaller : ScriptableObjectInstaller<DungeonSceneContextInstaller>
    {
        public override void InstallBindings()
        {
            PrefabSpawnerInstaller.Install(Container);
            BindEntitiesInScene();
        }
        
        private void BindEntitiesInScene()
        {
            Debug.Log("Bind Entities In Scene");
            Container.Bind<IEntityFactory>().To<EntityFactory>().AsSingle();

            Container.Bind<EntityMarker>().FromComponentsInHierarchy()
                .AsTransient()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<EntitySpawnService>().AsSingle();
        }
    }
}