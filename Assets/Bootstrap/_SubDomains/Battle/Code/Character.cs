using System.Collections;
using System.Collections.Generic;
using Atomic.Pathfinding.Core.Data;
using Atomic.Pathfinding.Core.Interfaces;
using UnityEngine;

public class Character : MonoBehaviour, IAgent
{
    public int Size => 1;
    
    
    public void OnPathResult(PathResult result)
    {
        //Do nothing
    }

}
