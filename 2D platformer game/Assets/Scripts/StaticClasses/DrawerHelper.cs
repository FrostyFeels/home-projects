using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DrawerHelper
{
    public static List<Tile> GetEdges(Vector3Int start, Vector3Int end, bool checkEnabled)
    {
        List<Tile> edges = new List<Tile>();
        int xDiffrence = Mathf.Abs(start.x - end.x);
        int zDiffrence = Mathf.Abs(start.z - end.z);

        if (zDiffrence == 0) zDiffrence = 1;
        if (xDiffrence == 0) xDiffrence = 1;

        //For getting the left to right edges
        for (int z = start.z; z <= end.z; z+=zDiffrence)
        {
            for (int x = start.x; x <= end.x; x++)
            {
                Vector3Int pos = new Vector3Int(x, start.y, z);
                if (TileManager.Tiles.TryGetValue(pos, out Tile tile))
                {
                    if(checkEnabled && !tile.IsSelected)
                    {
                        edges.Add(tile);
                    }      
                    
                    if(!checkEnabled && tile.IsSelected)
                    {
                        edges.Add(tile);
                    }
                }
            }
        }

        //for getting the up to down edges, the +1 and -1 are because the corners are already taken in the previous if statement
        for (int x = start.x; x <= end.x; x += xDiffrence)
        {
            for (int z = start.z + 1; z < end.z; z++)
            {
                Vector3Int pos = new Vector3Int(x, start.y, z);
                if (TileManager.Tiles.TryGetValue(pos, out Tile tile))
                {
                    if (checkEnabled && !tile.IsSelected)
                    {
                        edges.Add(tile);
                    }

                    if (!checkEnabled && tile.IsSelected)
                    {
                        edges.Add(tile);
                    }
                }
            }
        }
        return edges;
    }

    public static List<Tile> GetMiddle(Vector3Int start, Vector3Int end, bool checkEnabled)
    {
        List<Tile> middle = new List<Tile>();
        for (int z = start.z + 1; z <= end.z - 1; z++)
        {
            for (int x = start.x + 1; x <= end.x - 1; x++)
            {
                Vector3Int pos = new Vector3Int(x, start.y, z);
                if (TileManager.Tiles.TryGetValue(pos, out Tile tile))
                {
                    if (checkEnabled && !tile.IsSelected)
                    {
                        middle.Add(tile);
                    }

                    if (!checkEnabled && tile.IsSelected)
                    {
                        middle.Add(tile);
                    }
                }

            }
        }
        return middle;
    }

    public static Vector2Int OrderIndex(int start, int end)
    {
        if (start < end)
        {
            return new Vector2Int(start, end);
        }
        else
        {
            return new Vector2Int(end, start);
        }
    }
}
