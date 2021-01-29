using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputManager : MonoBehaviour
{
    public event Action<Character> CharacterDragStarted;
    public event Action CharacterDragEnded;
    public event Action<(int, int)> TileHovered;

    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _tileMask;
    [SerializeField] private float _maxRayDistance = 250f;

    [Inject(Id = "Player")] private Dictionary<string, Character> _playerCharacters;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CharacterDragEnded?.Invoke();
        }
        else if (IsCharacterClicked(out var character))
        {
            CharacterDragStarted?.Invoke(character);
        }

        var hoveredTileTransform = FindHoveredTile();

        if (hoveredTileTransform)
        {
            TileHovered?.Invoke(hoveredTileTransform.position.VectorToCoordinates());
        }
    }

    private bool IsCharacterClicked(out Character character)
    {
        character = null;

        if (!Input.GetMouseButtonDown(0))
        {
            return false;
        }

        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out var hit, _maxRayDistance, _playerMask))
            return false;

        character = _playerCharacters[hit.transform.name];
        return true;
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