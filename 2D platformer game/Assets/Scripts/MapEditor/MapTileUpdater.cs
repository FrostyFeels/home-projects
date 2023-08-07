using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileUpdater : MonoBehaviour
{
    public void UpdateMapEdges(Area area)
    {
        for (int i = TileManager.EdgeTiles.Count - 1; i >= 0; i--)
        {
            Tile tile = TileManager.EdgeTiles[i];
            if (tile.neighbours == 8)
            {
                tile.IsEdgeTile = false;             
                TileManager.middleTiles.Add(tile);
                TileManager.EdgeTiles.Remove(tile);                
            }
        }

        foreach (Layer layer in area.Layers)
        {
            foreach (Tile tile in layer.Tiles)
            {
                if(tile.neighbours < 8)
                {
                    tile.IsEdgeTile = true;
                    TileManager.EdgeTiles.Add(tile);
                }
                else
                {
                    tile.IsEdgeTile = false;
                    TileManager.middleTiles.Add(tile);
                }
            }
        }
    }

    public void UpdateTileStatus(ICollection<Tile> tiles, bool enabled)
    {
        foreach (Tile tile in tiles)
        {
            if (enabled)
            {
                tile.IsSelected = true;
            }
            else
            {
                tile.IsSelected = false;
            }
        }
    }
    
    public void UpdateMapHeight(ICollection<Tile> tiles, bool up) 
    {
        if(up)
        {
            foreach (Tile tile in tiles)
            {
                tile.Above?.Obj.SetActive(tile.IsSelected);
            }
        }
        else
        {
            foreach (Tile tile in tiles)
            {
                if(tile.Lower != null)
                {
                    tile.Obj.SetActive(tile.IsSelected);
                }            
            }
        }
    }
}
