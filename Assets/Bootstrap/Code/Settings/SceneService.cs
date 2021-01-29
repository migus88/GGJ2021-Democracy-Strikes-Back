using System.Collections.Generic;
using Bootstrap._SubDomains.Battle.Code.Settings;
using Cysharp.Threading.Tasks;
using ModestTree;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Bootstrap.Code.Settings
{
    
    
    public class SceneService : MonoBehaviour 
    {
        private readonly List<SceneSettings.SceneAssetReference> _currentlyLoadedScenes = new List<SceneSettings.SceneAssetReference>();
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
            await Addressables.LoadSceneAsync(scene, LoadSceneMode.Additive).ToUniTask();
            _currentlyLoadedScenes.Add(scene);
        }
        
        public async UniTask UnloadAllScenes()
        {
            if(_currentlyLoadedScenes.Count == 0)
                return;
            
            var tasks = new List<UniTask>();
            foreach (var scene in _currentlyLoadedScenes)
            {
                tasks.Add(Addressables.UnloadSceneAsync(scene.OperationHandle).ToUniTask());
            }

            await UniTask.WhenAll(tasks);
        }

        public async UniTask UnloadScene(SceneSettings.SceneAssetReference scene)
        {
            _currentlyLoadedScenes.RemoveWithConfirm(scene);
            await Addressables.UnloadSceneAsync(scene.OperationHandle).ToUniTask();
        }

        public async void OnDestroy()
        {
            await UnloadAllScenes();
            _currentlyLoadedScenes.Clear();
        }
    }

}