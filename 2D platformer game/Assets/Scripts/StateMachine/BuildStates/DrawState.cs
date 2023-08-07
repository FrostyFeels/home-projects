using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DrawState
{
    public abstract List<Tile> GetTiles(Vector3Int start, Vector3Int end, bool checkEnabled);
}
