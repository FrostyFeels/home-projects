using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Layer
{
	public int LayerIndex;
	public List<Tile> Tiles = new();

	public void CreateLayer(int length, int width, Vector2Int gridSpot)
	{
		for(int z = 0; z < width; z++)
		{
			for(int x = 0; x < length; x++)
			{
				Vector3Int pos = new Vector3Int(x + (length * gridSpot.x), LayerIndex, z + (width * gridSpot.y));

				Tiles.Add(new Tile()
				{
					Pos = pos
				});

			}	
		}
	}

	public void EnableLayer(bool isEnabled)
	{
		foreach(Tile tile in Tiles)
		{
			if(!tile.IsSelected)
            {
				tile.Obj.SetActive(isEnabled);
			}
		}
	}
}
