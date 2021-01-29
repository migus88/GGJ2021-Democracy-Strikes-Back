using System.Collections.Generic;
using Bootstrap.Code.Settings;
using Cysharp.Threading.Tasks;
using ModestTree;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Bootstrap.Code.Services
{
    public class SceneService : MonoBehaviour
    {
        private readonly List<SceneInstance> _currentlyLoadedScenes =
            new List<SceneInstance>();

        public async UniTask LoadScenes(SceneSettings settings)
        {
            await UnloadAllScenes();
            await LoadScene(settings.MainScene);
            var tasks = new List<UniTask>();

            foreach (var scene in settings.DependencyScenes)
            {
                tasks.Add(Addressables.LoadSceneAsync(scene, LoadSceneMode.Additive).ToUniTask());
            }

            await UniTask.WhenAll(tasks);
        }

        public async UniTask LoadScene(SceneSettings.SceneAssetReference scene)
        {
            var sceneInstance = await Addressables.LoadSceneAsync(scene, LoadSceneMode.Additive).ToUniTask();
            _currentlyLoadedScenes.Add(sceneInstance);
        }

        public async UniTask UnloadAllScenes()
        {
            if (_currentlyLoadedScenes.Count == 0)
                return;

            var tasks = new List<UniTask>();
            foreach (var scene in _currentlyLoadedScenes)
            {
                tasks.Add(Addressables.UnloadSceneAsync(scene).ToUniTask());
            }

            await UniTask.WhenAll(tasks);
        }

        public async UniTask UnloadScene(SceneSettings.SceneAssetReference scene)
        {
            await Addressables.UnloadSceneAsync(scene.OperationHandle).ToUniTask();
        }

        public async void OnDestroy()
        {
            _currentlyLoadedScenes.Clear();
        }
    }
}