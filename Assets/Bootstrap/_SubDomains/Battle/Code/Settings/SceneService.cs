using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using ModestTree;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Bootstrap._SubDomains.Battle.Code.Settings
{
    
    
    public class SceneService : MonoBehaviour 
    {
        //
        private List<SceneSettings.SceneAssetReference> _currentlyLoadedScenes;
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
            _currentlyLoadedScenes.Append(scene);
        }
        
        public async UniTask UnloadAllScenes()
        {
            if (_currentlyLoadedScenes.IsNullOrEmpty())
                _currentlyLoadedScenes = new List<SceneSettings.SceneAssetReference>();
            
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

        public async void Dispose()
        {
            await UnloadAllScenes();
            _currentlyLoadedScenes=null;
            Dispose();
        }
    }
}