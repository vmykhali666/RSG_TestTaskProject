using Content.Features.GameFlowStateMachineModule.Scripts;
using Core.AssetLoaderModule.Core.Scripts;
using Core.GlobalSignalsModule.Scripts;
using Core.SceneLoaderServiceModule.Scripts;
using UnityEngine;
using Zenject;

namespace Content.Features.GameBootstrapModule.Scripts.ProjectDI {
    [CreateAssetMenu(menuName = "Configurations/GameBootstrap/" + nameof(ProjectContextInstaller),
        fileName = nameof(ProjectContextInstaller) + "_Default", order = 0)]
    public class ProjectContextInstaller : ScriptableObjectInstaller<ProjectContextInstaller> {
        public override void InstallBindings() {
            BindGlobalSignals();
            SceneLoaderServiceModuleInstaller.Install(Container);
            GameFlowStateMachineInstaller.Install(Container);
            AssetLoaderInstaller.Install(Container);
        }

        private void BindGlobalSignals()
        {
            SignalBusInstaller.Install(Container);
            GlobalSignalsInstaller.Install(Container);
        }
    }
}
