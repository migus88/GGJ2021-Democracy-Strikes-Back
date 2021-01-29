using System;
using Bootstrap.Code.Services;
using Bootstrap.Code.Settings;
using UnityEngine;
using Zenject;

namespace Bootstrap.Code
{
    public class BootstrapController : MonoBehaviour
    {
        [SerializeField] private SceneSettings _sceneSettings;
        [SerializeField] private SoundSettings _soundSettings;
        [SerializeField] private Camera _camera;
        
        
        [Inject] private SceneService _sceneService;
        [Inject] private SoundService _soundService;

        private void Start()
        {
            LoadFirstScene();
        }


        private async void LoadFirstScene()
        {
            await _sceneService.LoadScenes(_sceneSettings);
            _camera.gameObject.SetActive(false);
        }
    }
}