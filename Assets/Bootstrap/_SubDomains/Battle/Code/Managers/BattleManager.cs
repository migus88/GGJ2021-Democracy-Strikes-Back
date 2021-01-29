using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleManager : MonoBehaviour
{
    [Inject]
    private InputManager _inputManager;

    private (int, int) _hoveredTileCoordinates = (-1, -1);
    
    private void Awake()
    {
        _inputManager.TileHovered += InputManagerOnTileHovered;
    }

    private void InputManagerOnTileHovered(Transform tileTransform)
    {
        var coordinates = tileTransform.position.VectorToCoordinates();
        
        if(coordinates == _hoveredTileCoordinates)
            return;

        _hoveredTileCoordinates = coordinates;
        
        Debug.Log($"Hovered: {_hoveredTileCoordinates}");
    }
}
