using System;
using Bootstrap.Code.Settings;
using UnityEngine;

namespace Bootstrap.Code
{
    public class StartSceneOnRun : MonoBehaviour
    {
        [SerializeField] private SceneService _sceneSystem;
        [SerializeField] private SceneSettings _sceneSettings;

        private void Start()
        {
            LoadFirstScene();
        }


        public async void LoadFirstScene()
        {
            await _sceneSystem.LoadScenes(_sceneSettings);
        }
    }
}