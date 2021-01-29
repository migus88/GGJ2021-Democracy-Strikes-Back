using System.Collections;
using System.Collections.Generic;
using Bootstrap.Code.Services;
using Bootstrap.Code.Settings;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private SceneService _sceneService;
    [SerializeField] private SoundService _soundService;
    
    public override void InstallBindings()
    {
        Container.Bind<SceneService>().FromInstance(_sceneService).AsSingle();
        Container.Bind<SoundService>().FromInstance(_soundService).AsSingle();
    }
}
