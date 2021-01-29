﻿using System;
using Bootstrap.Code.Settings;
using UnityEngine;
using Zenject;

namespace Bootstrap.Code
{
    public class BootstrapController : MonoBehaviour
    {
        [SerializeField] private SceneSettings _sceneSettings;
        [SerializeField] private Camera _camera;
        
        
        [Inject] private SceneService _sceneService;

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