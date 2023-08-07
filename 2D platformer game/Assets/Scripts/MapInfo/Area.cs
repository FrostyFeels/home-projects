using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Area
{
	public Vector2Int GridSpot;

	public Area North;
	public Area South;
	public Area East;
	public Area West;

	public List<Layer> Layers = new();
	public Tile[,,] AreaData;

	public void SetNeighbouringAreas(Vector2Int gridSpot)
    {
		GridSpot = gridSpot;
		if (AreaManager.Areas.TryGetValue(GridSpot + Vector2Int.up, out North))
		{
			North.South = this;
		}

		if (AreaManager.Areas.TryGetValue(GridSpot + Vector2Int.down, out South))
		{
			South.North = this;
		}

		if (AreaManager.Areas.TryGetValue(GridSpot + Vector2Int.right, out East))
		{
			East.West = this;
		}

		if (AreaManager.Areas.TryGetValue(GridSpot + Vector2Int.left, out West))
		{
			West.East = this;
		}


		AreaManager.Areas.Add(GridSpot, this);
	}
}

public static class AreaManager
{
	public static readonly Dictionary<Vector2Int, Area> Areas = new();
}