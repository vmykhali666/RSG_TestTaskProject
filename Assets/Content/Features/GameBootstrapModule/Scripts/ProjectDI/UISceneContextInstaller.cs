using Content.Features.UIModule.Scripts;
using UnityEngine;
using Zenject;

namespace Content.Features.GameBootstrapModule.Scripts.ProjectDI
{
    [CreateAssetMenu(menuName = "Configurations/GameBootstrap/" + nameof(UISceneContextInstaller),
        fileName = nameof(UISceneContextInstaller) + "_Default", order = 0)]
    public class UISceneContextInstaller : ScriptableObjectInstaller<UISceneContextInstaller> {
        public override void InstallBindings()
        {
            UIModuleInstaller.Install(Container);
        }
    }
}