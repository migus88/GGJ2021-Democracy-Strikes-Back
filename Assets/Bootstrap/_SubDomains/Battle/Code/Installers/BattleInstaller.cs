using System.Collections;
using System.Collections.Generic;
using Atomic.Pathfinding.Core;
using Atomic.Pathfinding.Core.Data;
using UnityEngine;
using Zenject;

public class BattleInstaller : MonoInstaller
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private BattleManager _battleManager;
    [SerializeField] private Field _field;
    [SerializeField] private FieldTile[] _tiles;
    [SerializeField] private Character[] _playerCharactes;


    public override void InstallBindings()
    {
        Container.Bind<InputManager>().FromInstance(_inputManager).AsSingle();
        Container.Bind<FieldTile[]>().FromInstance(_tiles).AsSingle();
        Container.Bind<Field>().FromInstance(_field).AsSingle();
        
        _field.UpdateMatrix();
        
        Container.Bind<AStar>().FromInstance(new AStar(_field, new PathfinderSettings
        {
            IsDiagonalMovementEnabled = false
        }));

        Container.Bind<BattleManager>().FromInstance(_battleManager).AsSingle();

        var playerCharacters = new Dictionary<string, Character>();

        foreach (var character in _playerCharactes)
        {
            playerCharacters.Add(character.name, character);
        }

        Container.Bind<Dictionary<string, Character>>().WithId("Player").FromInstance(playerCharacters).AsSingle();

        foreach (var tile in _tiles)
        {
            Container.Bind<FieldTile>().WithId(tile.Coordinates).FromInstance(tile);
        }
    }
}
