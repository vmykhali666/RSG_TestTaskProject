using Content.Features.AIModule.Scripts.Entity;
using Content.Features.StorageModule.Scripts;
using Content.Features.CameraModule;
using Content.Features.DamageablesModule.Scripts;
using Content.Features.InteractionModule;
using Content.Features.LootModule.Scripts;
using Content.Features.PersistanceModule;
using Content.Features.PlayerData.Scripts;
using Content.Features.PrefabSpawner;
using Content.Features.ShopModule.Scripts;
using UnityEngine;
using Zenject;

namespace Content.Features.GameBootstrapModule.Scripts.ProjectDI
{
    [CreateAssetMenu(menuName = "Configurations/GameBootstrap/" + nameof(GlobalSceneContextInstaller),
        fileName = nameof(GlobalSceneContextInstaller) + "_Default", order = 0)]
    public class GlobalSceneContextInstaller : ScriptableObjectInstaller<GlobalSceneContextInstaller>
    {
        public override void InstallBindings()
        {
            PrefabSpawnerInstaller.Install(Container);
            PlayerDataInstaller.Install(Container);
            PersistanceModuleInstaller.Install(Container);
            CameraInstaller.Install(Container);
            StorageModuleInstaller.Install(Container);
            DamageablesModuleInstaller.Install(Container);
            ShopModuleInstaller.Install(Container);
            InteractionSystemInstaller.Install(Container);
            AIInstaller.Install(Container);
            LootInstaller.Install(Container);
        }
    }
}