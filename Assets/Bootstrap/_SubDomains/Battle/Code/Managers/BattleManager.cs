using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Atomic.Pathfinding.Core;
using Atomic.Pathfinding.Core.Helpers;
using Bootstrap._SubDomains.Battle.Code.Data;
using Bootstrap._SubDomains.Battle.Code.Managers;
using Bootstrap._SubDomains.Battle.Code.Settings;
using UnityEngine;
using Zenject;

public class BattleManager : MonoBehaviour
{
    public Dictionary<string, Character> ActiveCharacters =>
        _turnManager.CurrentPlayerId == 0 ? _playerCharacters : _enemyCharacters;
    
    public Dictionary<string, Character> InactiveCharacters =>
        _turnManager.CurrentPlayerId == 1 ? _playerCharacters : _enemyCharacters;
    
    [SerializeField] private LevelConfig _levelConfig;
    
    [Inject] private InputManager _inputManager;
    [Inject] private DiContainer _container;
    [Inject] private FieldTile[] _tiles;
    [Inject] private Field _field;
    [Inject] private AStar _pathfinder;
    
    [Inject(Id = "Player")] private Dictionary<string, Character> _playerCharacters;
    [Inject(Id = "Enemy")] private Dictionary<string, Character> _enemyCharacters;
    [Inject(Id = "All")] private Dictionary<string, Character> _allCharacters;

    private (int, int) _hoveredTileCoordinates = (-1, -1);
    private Character _draggedCharacter;
    private List<PathCell> _highlightedPath;
    private TurnManager _turnManager;

    private void Awake()
    {
        _turnManager = new TurnManager(_levelConfig);
        _turnManager.RoundStarted += OnRoundStarted;

        foreach (var character in _playerCharacters)
        {
            character.Value.PlayerID = 0;
        }

        foreach (var character in _enemyCharacters)
        {
            character.Value.PlayerID = 1;
        }
        
        _inputManager.TileHovered += OnTileHovered;
        _inputManager.CharacterDragStarted += OnCharacterDragStarted;
        _inputManager.CharacterDragEnded += OnCharacterDragEnded;
        _inputManager.ActionCanceled += OnActionCanceled;
        
        UpdateOccupiedTiles();
        
        _turnManager.StartRound();
    }

    private void OnDestroy()
    {
        _turnManager.RoundStarted -= OnRoundStarted;
        
        _inputManager.TileHovered -= OnTileHovered;
        _inputManager.CharacterDragStarted -= OnCharacterDragStarted;
        _inputManager.CharacterDragEnded -= OnCharacterDragEnded;
        _inputManager.ActionCanceled -= OnActionCanceled;
    }

    public void OnCharacterFinishedMovement()
    {
        _highlightedPath = null;
        UpdateOccupiedTiles();

        if (ActiveCharacters.All(c => c.Value.ActionPoints <= 0))
        {
            _turnManager.TurnEnded();
            OnTileHovered((-1, -1));
        }
    }
    
    public void HighlightPath(List<PathCell> path)
    {
        _highlightedPath = path;
        
        foreach (var tile in _tiles)
        {
            var index = path.FindIndex(c => c.Coordinates == tile.Coordinates);
            
            if (index >= 0)
            {
                tile.OnPathHighlight(index, path[index].IsCharacter);
                //TODO: Highlight trajectory
            }
            else
            {
                tile.OnPathCleared();
            }
        }
    }
    
    #region Private methods

    private void OnRoundStarted()
    {
        foreach (var character in ActiveCharacters)
        {
            character.Value.InitActionPoints();
        }

        foreach (var character in InactiveCharacters)
        {
            character.Value.ClearActionPoints();
        }
    }

    private void UpdateOccupiedTiles()
    {
        foreach (var tile in _tiles)
        {
            tile.IsOccupied = false;
        }
        
        foreach (var character in _allCharacters)
        {
            var coords = character.Value.Origin;
            _field.TileMatrix[coords.Y(), coords.X()].IsOccupied = true;
        }
    }

    private void OnActionCanceled()
    {
        _highlightedPath = null;
        OnCharacterDragEnded();
    }

    private void OnCharacterDragEnded()
    {
        if (!_draggedCharacter || _turnManager.CurrentPlayerId != 0)
            return;

        if (_highlightedPath != null && _highlightedPath.Count > 1)
        {
            _draggedCharacter.Move(_highlightedPath);
        }

        _draggedCharacter = null;
        ClearPathHighlight();
    }

    private void OnCharacterDragStarted(Character character)
    {
        if(_turnManager.CurrentPlayerId != 0)
            return;
        
        _draggedCharacter = character;
    }

    private void ClearPathHighlight()
    {
        foreach (var tile in _tiles)
        {
            tile.OnPathCleared();
        }
    }

    private void OnTileHovered((int, int) coordinates)
    {
        if(_turnManager.CurrentPlayerId != 0)
            return;
        
        if (coordinates == _hoveredTileCoordinates)
            return;

        if (_draggedCharacter)
        {
            _draggedCharacter.FindPath(coordinates);
        }
        else
        {
            _hoveredTileCoordinates = coordinates;

            foreach (var tile in _tiles)
            {
                if (tile.Coordinates == coordinates)
                {
                    tile.OnHover();
                }
                else
                {
                    tile.OnHoverLeave();
                }
            }
        }
    }
    
    #endregion
}