using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Atomic.Pathfinding.Core;
using Atomic.Pathfinding.Core.Data;
using Atomic.Pathfinding.Core.Helpers;
using Atomic.Pathfinding.Core.Interfaces;
using Bootstrap._SubDomains.Battle.Code.Controllers;
using Bootstrap._SubDomains.Battle.Code.Data;
using Bootstrap._SubDomains.Battle.Code.Settings;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour, IAgent
{
    public int Size => 1;
    public (int, int) Origin => transform.position.VectorToCoordinates();
    public int PlayerID { get; set; }
    public int ActionPoints { get; private set; }

    [SerializeField] private CharacterSettings _settings;
    [SerializeField] private CharacterMovementController _movementController;

    [Inject] private AStar _pathfinding;
    [Inject] private BattleManager _battleManager;
    [Inject] private Field _field;
    
    private List<PathCell> _highlightedPath;

    private readonly ConcurrentDictionary<(int, int), List<PathCell>> _availablePaths =
        new ConcurrentDictionary<(int, int), List<PathCell>>();

    private void Awake()
    {
        _movementController.Settings = _settings;
    }

    public void UseActionPoints(int amount)
    {
        if (amount < 0)
            throw new Exception("The amount cannot be negative");
        
        if (ActionPoints < amount)
            throw new Exception("Can't use more action points then available");

        ActionPoints -= amount;
    }

    public void InitActionPoints()
    {
        ActionPoints = _settings.MaxActionPoints;
    }

    public void ClearActionPoints()
    {
        ActionPoints = 0;
    }

    public async void FindPath((int, int) destination)
    {
        if (!_availablePaths.TryGetValue(destination, out var path))
        {
            var pathResult = await _pathfinding.GetPathAsync(this, Origin, destination);

            if (!pathResult.IsPathFound || (pathResult.Path.Count - 1) > ActionPoints)
                return;

            path = GetCalculatedPath(pathResult.Path);
            
            _availablePaths.TryAdd(destination, path);
        }
        
        if(path.Count > _settings.MaxActionPoints + 1)
            return;
        
        _battleManager.HighlightPath(path);
    }

    private List<PathCell> GetCalculatedPath(List<(int, int)> path)
    {
        if (path.Count == 1)
        {
            return new List<PathCell>
            {
                new PathCell
                {
                    Coordinates = path[0]
                }
            };
        }
        
        var calculated = new List<PathCell>(path.Count);

        for (int i = 0; i < path.Count; i++)
        {
            var coord = path[i];
            var tile = _field.TileMatrix[coord.Y(), coord.X()];
            var cell = new PathCell
            {
                Coordinates = coord, 
                IsCharacter = tile.IsOccupied && coord != Origin
            };

            if (i < path.Count - 1)
            {
                var next = path[i + 1];
                cell.Direction = (next.X() - coord.X(), next.Y() - coord.Y());
            }
            else
            {
                var prev = path[i - 1];
                cell.Direction = (coord.X() - prev.X(), coord.Y() - prev.Y());
            }
            
            calculated.Add(cell);
        }

        return calculated;
    }

    public async void Move(List<PathCell> path)
    {
        UseActionPoints(path.Count - 1);
        await _movementController.MoveToPath(path);
        _availablePaths.Clear();
    }

    public void OnPathResult(PathResult result)
    {
        //Do nothing
    }
}