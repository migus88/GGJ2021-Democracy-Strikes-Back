using System;
using System.Collections;
using System.Collections.Generic;
using Atomic.Pathfinding.Core;
using Atomic.Pathfinding.Core.Helpers;
using UnityEngine;
using Zenject;

public class BattleManager : MonoBehaviour
{
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
    private List<(int, int)> _highlightedPath;

    private void Awake()
    {
        _inputManager.TileHovered += OnTileHovered;
        _inputManager.CharacterDragStarted += OnCharacterDragStarted;
        _inputManager.CharacterDragEnded += OnCharacterDragEnded;
        _inputManager.ActionCanceled += OnActionCanceled;
        
        UpdateOccupiedTiles();
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
        if (!_draggedCharacter)
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
        _draggedCharacter = character;
    }

    public void OnCharacterFinishedMovement()
    {
        _highlightedPath = null;
        UpdateOccupiedTiles();
    }

    public void ClearPathHighlight()
    {
        foreach (var tile in _tiles)
        {
            tile.OnPathCleared();
        }
    }

    public void HighlightPath(List<(int, int)> path)
    {
        _highlightedPath = path;
        
        foreach (var tile in _tiles)
        {
            var index = path.IndexOf(tile.Coordinates);
            if (index >= 0)
            {
                tile.OnPathHighlight(index);
            }
            else
            {
                tile.OnPathCleared();
            }
        }
    }

    private void OnTileHovered((int, int) coordinates)
    {
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
}