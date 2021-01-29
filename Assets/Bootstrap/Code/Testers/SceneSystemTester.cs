using Bootstrap.Code.Services;
using Bootstrap.Code.Settings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bootstrap.Code.Testers
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