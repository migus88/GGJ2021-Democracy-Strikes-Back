using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorHelpers
{
    public static (int, int) VectorToCoordinates(this Vector3 vector3)
    {
        return ((int) vector3.z, (int) vector3.x);
    }
}
