using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStarDistanceHelper
{
    public static int GetDistance(Vector3Int start, Vector3Int end)
    {
        int distance = Mathf.Abs(start.x - end.x) + Mathf.Abs(start.z - end.z);
        return distance;
    }
}
