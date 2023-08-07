using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileManager
{
    public static readonly Dictionary<Vector3Int, Tile> Tiles = new();
    public static readonly List<Tile> middleTiles = new();
    public static readonly List<Tile> EdgeTiles = new();
    public static readonly List<Tile> SelectedTiles = new();
}
