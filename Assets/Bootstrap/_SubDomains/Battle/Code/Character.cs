using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Atomic.Pathfinding.Core;
using Atomic.Pathfinding.Core.Data;
using Atomic.Pathfinding.Core.Interfaces;
using Bootstrap._SubDomains.Battle.Code.Controllers;
using Bootstrap._SubDomains.Battle.Code.Settings;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour, IAgent
{
    public int Size => 1;
    public (int, int) Origin => transform.position.VectorToCoordinates();

    [SerializeField] private CharacterSettings _settings;
    [SerializeField] private CharacterMovementController _movementController;

    [Inject] private AStar _pathfinding;
    [Inject] private BattleManager _battleManager;
    
    
    private List<(int, int)> _highlightedPath;

    private readonly ConcurrentDictionary<(int, int), List<(int, int)>> _availablePaths =
        new ConcurrentDictionary<(int, int), List<(int, int)>>();

    public async void FindPath((int, int) destination)
    {
        if (!_availablePaths.TryGetValue(destination, out var path))
        {
            var pathResult = await _pathfinding.GetPathAsync(this, Origin, destination);

            if (!pathResult.IsPathFound)
                return;

            path = pathResult.Path;
            _availablePaths.TryAdd(destination, path);
        }
        
        if(path.Count > _settings.MaxMovementDistance + 1)
            return;
        
        _battleManager.HighlightPath(path);
    }

    public async void Move(List<(int, int)> path)
    {
        await _movementController.MoveToPath(path);
        _availablePaths.Clear();
    }

    public void OnPathResult(PathResult result)
    {
        //Do nothing
    }
}