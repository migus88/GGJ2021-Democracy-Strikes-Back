using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Bootstrap._SubDomains.Battle.Code.Settings
{
    [CreateAssetMenu(fileName = "Dummy", menuName = "Atomic/Dummy Settings", order = 0)]
    public class DummySettings : ScriptableObject
    {
        public AssetReferenceT<SceneAsset> MainScene;
        public AssetReferenceT<SceneAsset>[] DependencyScenes;

        [System.Serializable]
        public class SceneAssetReference : AssetReferenceT<SceneAsset>
        {
            public SceneAssetReference(string guid) : base(guid)
            {
            }
        }
    }

    public class SceneSystem : MonoBehaviour
    {
        //
        
        public async UniTask LoadScene(DummySettings settings)
        {
            var tasks = new List<UniTask>();

            foreach (var scene in settings.DependencyScenes) 
            {
                tasks.Add(Addressables.LoadSceneAsync(scene, LoadSceneMode.Additive).ToUniTask());
            }

            await UniTask.WhenAll(tasks);
        }
    }
}