using System.Collections.Generic;
using Bootstrap._SubDomains.Battle.Code.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Bootstrap._SubDomains.Battle.Code.Settings
{
    public class SceneSystem : MonoBehaviour 
    {
        //
        private SceneSettings.SceneAssetReference[] _currentlyLoadedScenes;
        public async UniTask LoadScenes(SceneSettings settings)
        {
            var tasks = new List<UniTask>();

            foreach (var scene in settings.DependencyScenes) 
            {
                tasks.Add(Addressables.LoadSceneAsync(scene, LoadSceneMode.Additive).ToUniTask());
            }

            await UniTask.WhenAll(tasks);
        }

        public async void UnloadAllScenes()
        {
            var tasks = new List<UniTask>();
            foreach (var scene in _currentlyLoadedScenes)
            {
                tasks.Add(Addressables.UnloadSceneAsync(scene.OperationHandle).ToUniTask());
            }
            
            await UniTask.WhenAll(tasks);
        }

        public void UnloadScene()
        {
            throw new System.NotImplementedException();
        }
    }
}