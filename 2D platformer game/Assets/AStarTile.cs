using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AStarTile
{
    public int GCost;
    public int HCost;

    public Vector3Int GridSpot;
    public bool Walkable = true;

    [NonSerialized] public List<AStarTile> MyNeighbours = new();
    [NonSerialized] public AStarTile parent;
    public Vector3Int North, South, West, East;
    [NonSerialized] public Tile tile;

    public AStarTile(Vector3Int gridSpot)
    {
        GridSpot = gridSpot;
    }

    public float FCost
    {
        get
        {
            return GCost + HCost;
        }
    }
}
