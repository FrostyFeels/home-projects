using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingState : DrawState
{
    public override List<Tile> GetTiles(Vector3Int start, Vector3Int end, bool checkEnabled)
    {
        List<Tile> tiles = DrawerHelper.GetEdges(start, end, checkEnabled);
        tiles.AddRange(DrawerHelper.GetMiddle(start, end, checkEnabled));

        return tiles;
    }
}
