using System;
using System.Collections;
using System.Collections.Generic;
using Atomic.Pathfinding.Core;
using UnityEngine;
using Zenject;

public class BattleManager : MonoBehaviour
{
    [Inject] private InputManager _inputManager;
    [Inject] private DiContainer _container;
    [Inject] private FieldTile[] _tiles;
    [Inject] private Field _field;
    [Inject] private AStar _pathfinder;

    private (int, int) _hoveredTileCoordinates = (-1, -1);
    private Character _draggedCharacter;
    private List<(int, int)> _highlightedPath;

    private void Awake()
    {
        _inputManager.TileHovered += OnTileHovered;
        _inputManager.CharacterDragStarted += OnCharacterDragStarted;
        _inputManager.CharacterDragEnded += OnCharacterDragEnded;
        _inputManager.ActionCanceled += OnActionCanceled;
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
            if (index >= 0)//path.Contains(tile.Coordinates))
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