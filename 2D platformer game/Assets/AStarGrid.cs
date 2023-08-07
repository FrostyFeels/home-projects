using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarGrid : MonoBehaviour
{
    public Dictionary<Vector3Int, AStarTile> tiles = new();
    public List<AStarTile> listTiles = new();

    public void CreateGrid()
    {
        foreach (Tile tile in TileManager.Tiles.Values)
        {
            AStarTile aTile = new AStarTile(tile.Pos);
            tiles.Add(tile.Pos, aTile);
            listTiles.Add(aTile);

            if (tile.Above != null)
            {
                aTile.Walkable = false;
            }

            aTile.tile = tile;
        }
        GetNeighBours();
    }

    private void GetNeighBours()
    {
        foreach (Tile tile in TileManager.Tiles.Values)
        {
            AStarTile aTile = tiles[tile.Pos];

            GetDirection(aTile, tile.Pos + Vector3Int.forward);
            GetDirection(aTile, tile.Pos + Vector3Int.back);
            GetDirection(aTile, tile.Pos + Vector3Int.right);
            GetDirection(aTile, tile.Pos + Vector3Int.left);
        }
    }

    private void GetDirection(AStarTile aTile, Vector3Int startPos)
    {      
        if (TileManager.Tiles.TryGetValue(startPos + Vector3Int.up, out Tile upperTile) && tiles[upperTile.Pos].Walkable)
        {
            aTile.MyNeighbours.Add(tiles[upperTile.Pos]);
        }
        else if (TileManager.Tiles.TryGetValue(startPos, out Tile tile) && tiles[tile.Pos].Walkable)
        {
            aTile.MyNeighbours.Add(tiles[tile.Pos]);
        }
        else if(TileManager.Tiles.TryGetValue(startPos + Vector3Int.down, out Tile lowerTile) && tiles[lowerTile.Pos].Walkable)
        {
            aTile.MyNeighbours.Add(tiles[lowerTile.Pos]);
        }
    }
}
