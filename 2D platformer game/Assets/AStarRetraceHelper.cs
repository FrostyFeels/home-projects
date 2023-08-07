using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public static class AStarRetraceHelper
{
    public static List<Vector3Int> RetracePath(AStarTile startTile, AStarTile endTile)
    {
        List<Vector3Int> path = new();
        AStarTile currentNode = endTile;

        while (currentNode != startTile)
        {
            path.Add(currentNode.GridSpot);
            currentNode = currentNode.parent;
        }
        path.Add(startTile.GridSpot);
        path.Reverse();
        return path;
    }
}
