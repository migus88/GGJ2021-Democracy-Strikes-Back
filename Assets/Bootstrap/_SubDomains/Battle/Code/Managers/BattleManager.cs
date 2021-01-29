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


    private AStar _pathfinder;
    private (int, int) _hoveredTileCoordinates = (-1, -1);
    private FieldTile _hoveredTile;
    private Character _draggedCharacter;

    private void Awake()
    {
        _pathfinder = new AStar(_field);
        
        _inputManager.TileHovered += InputManagerOnTileHovered;
        _inputManager.CharacterDragStarted += InputManagerOnCharacterDragStarted;
        _inputManager.CharacterDragEnded += InputManagerOnCharacterDragEnded;
    }

    private void InputManagerOnCharacterDragEnded()
    {
        if(!_draggedCharacter)
            return;
        
        _draggedCharacter = null;
        Debug.Log("Stopped Dragging");
    }

    private void InputManagerOnCharacterDragStarted(Character character)
    {
        _draggedCharacter = character;
        Debug.Log("Dragging");
    }

    private void InputManagerOnTileHovered((int, int) coordinates)
    {
        if (coordinates == _hoveredTileCoordinates)
            return;

        _hoveredTileCoordinates = coordinates;
        
        foreach (var tile in _tiles)
        {
            if(tile.Coordinates == coordinates)
            {
                _hoveredTile = tile;
                tile.OnHover();
            }
            else
            {
                tile.OnHoverLeave();
            }
            
        }
    }
}