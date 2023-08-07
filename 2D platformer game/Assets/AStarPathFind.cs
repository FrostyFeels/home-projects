using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class AStarPathFind : MonoBehaviour
{
    [SerializeField] private AStarGrid grid;

    public List<Vector3Int> FindPath(Vector3Int startPosition, Vector3Int endPosition)
    {

        AStarTile startTile = grid.tiles[startPosition];
        AStarTile endTile = grid.tiles[endPosition];

        List<AStarTile> openSet = new List<AStarTile>();
        HashSet<AStarTile> closedSet = new HashSet<AStarTile>();
        openSet.Add(startTile);


        while (openSet.Count > 0)
        {
            AStarTile currentTile = openSet[0];
            for (int i = 0; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentTile.FCost || openSet[i].FCost == currentTile.FCost && openSet[i].HCost < currentTile.HCost)
                {
                    currentTile = openSet[i];
                }
            }

            openSet.Remove(currentTile);
            closedSet.Add(currentTile);

            if(currentTile == endTile)
            {
                return AStarRetraceHelper.RetracePath(startTile, endTile);
            }

            foreach (AStarTile neighbour in currentTile.MyNeighbours)
            {
                if (!neighbour.Walkable || closedSet.Contains(neighbour)) continue;
                int GCost = currentTile.GCost + AStarDistanceHelper.GetDistance(currentTile.GridSpot, neighbour.GridSpot);
                if (GCost < neighbour.GCost || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = GCost;
                    neighbour.HCost = AStarDistanceHelper.GetDistance(neighbour.GridSpot, endTile.GridSpot);
                    neighbour.parent = currentTile;

                    if (!openSet.Contains(neighbour))
                    {                        
                        openSet.Add(neighbour);                       
                    }
                                         
                }
            }
        }
        
        return null;
    }
}
