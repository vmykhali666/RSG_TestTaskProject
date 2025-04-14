using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Core.SceneLoaderServiceModule.Scripts {
    public interface ISceneLoaderService {
        public UniTask LoadSceneAsync(string sceneToLoad, bool unloadRedundant);
        public UniTask LoadScenesAsync(List<string> scenesToLoad, string activeScene, bool unloadRedundant);

        public UniTask UnloadSceneAsync(string sceneToUnload);
        public UniTask UnloadScenesAsync(List<string> scenesToUnload);
    }
}
