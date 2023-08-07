using System;
using UnityEngine;


[Serializable]
public class Tile
{
    public Vector3Int Pos;

    public bool IsEdgeTile = false;
    public bool IsSelected = false;

    public GameObject Obj;
    public Renderer Render;
    public MeshFilter filter;
    public Material defaultMaterial;

    public int neighbours;

    public Tile North;
    public Tile South;
    public Tile East;
    public Tile West;
    public Tile NE;
    public Tile NW;
    public Tile SE;
    public Tile SW;
    public Tile Above;
    public Tile Lower;

    public void SetTileNeighbours()
    {
        if (GetNeighbours(Vector3Int.forward,ref North))
        {
            North.South = this;
        }

        if (GetNeighbours(Vector3Int.back, ref South))
        {
            South.North = this;
        }

        if (GetNeighbours(Vector3Int.right,ref East))
        {
            East.West = this;
        }

        if (GetNeighbours(Vector3Int.left, ref West))
        {
            West.East = this;
        }

        if (GetNeighbours(Vector3Int.forward + Vector3Int.right, ref NE))
        {
            NE.SW = this;
        }

        if (GetNeighbours(Vector3Int.forward + Vector3Int.left, ref NW))
        {
            NW.SE = this;
        }

        if (GetNeighbours(Vector3Int.back + Vector3Int.right, ref SE))
        {
            SE.NW = this;
        }

        if (GetNeighbours(Vector3Int.back + Vector3Int.left, ref SW))
        {
            SW.NE = this;
        }
    }

    public bool GetNeighbours(Vector3Int dir, ref Tile neighbour)
    {
        if (neighbour == null && TileManager.Tiles.TryGetValue(Pos + dir, out neighbour))
        {      
            neighbour.neighbours++;
            neighbours++;
            return true;
        }
        return false;
    }

    public void GetUpperNeighbour()
    {
        if (TileManager.Tiles.TryGetValue(Pos + Vector3Int.up, out Tile tile))
        {
            Above = tile;
            tile.Lower = this;
        }  
    }

    public void GetLowerNeighbour()
    {
        if(Pos.y == 0)
        {
            return;
        }
        else
        {
            if(TileManager.Tiles.TryGetValue(Pos + Vector3Int.down, out Tile tile))
            {
                Lower = tile;
                tile.Above = this;
            }
        }
    }
}

