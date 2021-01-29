using System.Collections;
using System.Collections.Generic;
using Atomic.Pathfinding.Core.Helpers;
using Bootstrap._SubDomains.Battle.Code.Data;
using UnityEngine;

public static class VectorHelpers
{
    public static (int, int) VectorToCoordinates(this Vector3 vector3)
    {
        return ((int) vector3.z, (int) vector3.x);
    }

    public static Vector3 CoordinatesToVector(this (int, int) coordinates, float y = 0f)
    {
        return new Vector3(coordinates.Y(), y, coordinates.X());
    }

    public static Vector3[] CoordinatesToVector(this List<(int, int)> coordinates, float y = 0f)
    {
        var path = new Vector3[coordinates.Count];

        for (int i = 0; i < coordinates.Count; i++)
        {
            path[i] = coordinates[i].CoordinatesToVector(y);
        }

        return path;
    }

    public static Vector3[] CoordinatesToVector(this List<PathCell> coordinates, float y = 0f)
    {
        var path = new Vector3[coordinates.Count];

        for (int i = 0; i < coordinates.Count; i++)
        {
            path[i] = coordinates[i].Coordinates.CoordinatesToVector(y);
        }

        return path;
    }
}