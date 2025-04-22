using Content.Features.PlayerData.Scripts.Datas;
using Zenject;

namespace Content.Features.AIModule.Scripts.Entity {
    public class AIInstaller : Installer<AIInstaller> {
        public override void InstallBindings() {
            Container.Bind<PlayerEntityModel>()
                .AsSingle();

            Container.Bind<IEntityBehaviourFactory>()
                .To<EntityBehaviourFactory>()
                .AsSingle();
            
            Container.Bind<IEntityDataService>()
                .To<EntityDataService>()
                .AsSingle();
        }
    }
}