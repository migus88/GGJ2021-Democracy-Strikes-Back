using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action<Character> CharacterDragStarted;
    public event Action<(int, int)> TileHovered;

    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _tileMask;
    [SerializeField] private float _maxRayDistance = 250f;


    private void Update()
    {
        var hoveredTileTransform = FindHoveredTile();

        if (hoveredTileTransform)
        {
            TileHovered?.Invoke(hoveredTileTransform.position.VectorToCoordinates());
        }
    }

    private Transform FindHoveredTile()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, _maxRayDistance, _tileMask))
        {
            return hit.transform;
        }

        return null;
    }
}