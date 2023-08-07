using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyState : DrawState
{
    public override List<Tile> GetTiles(Vector3Int start, Vector3Int end, bool checkEnabled)
    {
        return DrawerHelper.GetEdges(start, end, checkEnabled);
    }
}
