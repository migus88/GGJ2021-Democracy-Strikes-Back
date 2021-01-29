using System;
using System.Collections;
using System.Collections.Generic;
using Atomic.Pathfinding.Core.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Field : MonoBehaviour, IGrid
{
    public IGridCell[,] Matrix { get; private set; }

    [SerializeField] private Vector2 _cellSize;
    [SerializeField] private int _columnsCount;
    [SerializeField] private FieldTile[] _tiles;
    
    public void UpdateMatrix()
    {
        var rowsCount = (int) Math.Ceiling((double) _tiles.Length / _columnsCount);

        Matrix = new IGridCell[_columnsCount, rowsCount];

        var y = 0;
        var x = 0;

        foreach (var tile in _tiles)
        {
            Matrix[y, x] = tile;

            y++;

            if (y != _columnsCount)
                continue;

            y = 0;
            x++;
        }
    }


    #if UNITY_EDITOR
    [Button]
    public void UpdateFieldPositions()
    {
        var currentCol = 0;
        var currentRow = 0;

        foreach (FieldTile tile in _tiles)
        {
            tile.transform.position = new Vector3(_cellSize.x * currentCol, 0, _cellSize.y * currentRow);
            tile.Coordinates = (currentRow, currentCol);

            currentCol++;

            if (currentCol == _columnsCount)
            {
                currentCol = 0;
                currentRow++;
            }

            UnityEditor.EditorUtility.SetDirty(tile);
        }
    }
    #endif
}