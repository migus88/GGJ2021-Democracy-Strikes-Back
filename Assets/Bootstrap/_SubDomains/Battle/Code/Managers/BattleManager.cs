using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleManager : MonoBehaviour
{
    [Inject] private InputManager _inputManager;
    [Inject] private DiContainer _container;

    private (int, int) _hoveredTileCoordinates = (-1, -1);
    private FieldTile _hoveredTile;

    private void Awake()
    {
        _inputManager.TileHovered += InputManagerOnTileHovered;
    }

    private void InputManagerOnTileHovered((int, int) coordinates)
    {
        if (coordinates == _hoveredTileCoordinates)
            return;

        _hoveredTileCoordinates = coordinates;
        _hoveredTile = _container.ResolveId<FieldTile>(_hoveredTileCoordinates);
        _hoveredTile.OnHover();
    }
}