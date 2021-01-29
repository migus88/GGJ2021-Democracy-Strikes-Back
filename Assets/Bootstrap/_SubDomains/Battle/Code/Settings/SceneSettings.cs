using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Bootstrap._SubDomains.Battle.Code.Settings
{
    [CreateAssetMenu(fileName = "Scene", menuName = "Atomic/Settings", order = 0)]
    public class SceneSettings : ScriptableObject
    {
        public SceneAssetReference MainScene;
        public SceneAssetReference[] DependencyScenes;

        [System.Serializable]
        public class SceneAssetReference : AssetReferenceT<SceneAsset>
        {
            public SceneAssetReference(string guid) : base(guid)
            {
            }
        }
    }
}