using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class MapGenerator : MonoBehaviour
{
	public UnityAction OnGenerateMap;

	public string MapName;

	[Header("scripts")]
	[SerializeField] private ExpandMap _expandPrefab;
	[SerializeField] private MapManager _manager;
	[SerializeField] private TileStats _tilePrefab;

    [Header("Prefab")]
	public GameObject MapHolder;
	[SerializeField] private Transform _edgeHolder;

    public void GenerateMap(Area area, Vector2Int mapSize)
	{
		int layerCount = 0;
		GameObject areaHolder = GenerateMapHolder("Area", area.GridSpot.ToString(), MapHolder.transform);

		foreach(Layer layer in area.Layers)
		{
			GameObject layerHolder = GenerateMapHolder("layer", layerCount.ToString(), areaHolder.transform);
		
			foreach(Tile tile in layer.Tiles)
			{
				SetTileStats(tile, layerHolder.transform, area, mapSize);
			}

			if(layerCount == 0)
			{
				layer.EnableLayer(true);
			}
			else
			{
				layer.EnableLayer(false);
			}
			layerCount++;
		}

		GenerateEdges(area, mapSize);
		_manager.Updater.UpdateMapEdges(area);
		_manager.Showcaser.ShowCaseMap(TileManager.Tiles.Values);

	}

	//This is to create a structure in Unity for where tiles are selected
	private GameObject GenerateMapHolder(string holderName, string holderIndex, Transform holderParent)
    {
		GameObject holder = new()
		{
			name = holderName + holderIndex,
			transform =
			{
				parent = holderParent
			}
		};
		return holder;
    }

	//Generate the expand edges so that the player can select to increase the mapsize
	private void GenerateEdges(Area area, Vector2Int mapSize)
	{
		Vector2 middle = new Vector2((float)(mapSize.x -1) / 2, (float)(mapSize.y - 1) / 2) + new Vector2Int(mapSize.x, mapSize.y) * area.GridSpot;
		
		if(area.North == null)
		{
			SetExpanders(area, middle, Vector2Int.up, mapSize.y);
		}

		if(area.South == null)
		{
			SetExpanders(area, middle, Vector2Int.down, mapSize.y);
		}

		if(area.West == null)
		{
			SetExpanders(area, middle, Vector2Int.left, mapSize.x);
		}

		if(area.East == null)
		{
			SetExpanders(area, middle, Vector2Int.right, mapSize.x);
		}

		OnGenerateMap.Invoke();
	}
	
	//set the expanders position and initializes them
	private void SetExpanders(Area area, Vector2 middle, Vector2Int direction, float mapSize)
	{
		const float offset = .5f;
		
		Vector2 position = middle + (Vector2)direction * (mapSize / 2 + offset) + (Vector2)direction;
		ExpandMap expandMap = Instantiate(_expandPrefab);

	

		expandMap.transform.localScale = new Vector3(1 + (7 * Mathf.Abs(direction.y)), 1, 1 + (7 * Mathf.Abs(direction.x)));
		expandMap.Init(_manager.map, area.GridSpot + direction, this);
		expandMap.transform.position = new Vector3(position.x, 0, position.y);
		expandMap.transform.parent = _edgeHolder;
		_manager.map.ExpandMaps.Add(expandMap);
	}

	private void SetTileStats(Tile tile, Transform layerHolder, Area area, Vector2Int mapSize)
	{ 
		TileStats newTile = Instantiate(_tilePrefab, layerHolder);
		newTile.GridId = tile.Pos;
		newTile.transform.position = tile.Pos;

		tile.neighbours = 0;
		tile.SetTileNeighbours();
		tile.GetLowerNeighbour();
		tile.GetUpperNeighbour();
		tile.Render = newTile.gameObject.GetComponent<Renderer>();
		tile.filter = newTile.gameObject.GetComponent<MeshFilter>();
		tile.Obj = newTile.gameObject;

		TileManager.Tiles.Add(tile.Pos, tile);
	}
}
