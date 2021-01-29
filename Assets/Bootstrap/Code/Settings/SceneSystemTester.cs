using Sirenix.OdinInspector;
using UnityEngine;

namespace Bootstrap.Code.Settings
{
    public class SceneSystemTester : MonoBehaviour
    {
        [SerializeField] private SceneService _sceneSystem;
        [SerializeField] private SceneSettings _sceneSettings;


        [Button]
        public async void LoadBattleScene()
        {
            await _sceneSystem.LoadScenes(_sceneSettings);
        }
    }
}