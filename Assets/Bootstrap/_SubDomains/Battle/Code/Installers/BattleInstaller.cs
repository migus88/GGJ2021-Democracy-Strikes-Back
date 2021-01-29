using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleInstaller : MonoInstaller
{
    [SerializeField] private InputManager _inputManager;
    

    public override void InstallBindings()
    {
        Container.Bind<InputManager>().FromInstance(_inputManager).AsSingle();
    }
}
