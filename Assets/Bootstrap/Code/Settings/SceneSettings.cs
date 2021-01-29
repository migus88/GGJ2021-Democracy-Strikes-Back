using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Bootstrap.Code.Settings
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