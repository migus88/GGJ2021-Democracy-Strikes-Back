using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleManager : MonoBehaviour
{
    [Inject]
    private InputManager _inputManager;

    private void Awake()
    {
        _inputManager.TileHovered += InputManagerOnTileHovered;
    }

    private void InputManagerOnTileHovered(Transform tileTransform)
    {
        Debug.Log($"Hovered: {tileTransform.position.VectorToCoordinates()}");
    }
}
