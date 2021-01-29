using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleInstaller : MonoInstaller
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private FieldTile[] _tiles;
    

    public override void InstallBindings()
    {
        Container.Bind<InputManager>().FromInstance(_inputManager).AsSingle();

        foreach (var tile in _tiles)
        {
            Container.Bind<FieldTile>().WithId(tile.Coordinates).FromInstance(tile);
        }
    }
}
