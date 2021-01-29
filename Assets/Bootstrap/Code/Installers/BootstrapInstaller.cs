using System.Collections;
using System.Collections.Generic;
using Bootstrap.Code.Settings;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private SceneService _sceneService;
    
    public override void InstallBindings()
    {
        Container.Bind<SceneService>().FromInstance(_sceneService).AsSingle();
    }
}
