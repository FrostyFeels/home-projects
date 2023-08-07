using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("Map")]
	[SerializeField] private MapManager _manager;

    [Header("Size")]
	[SerializeField] private int _areaXSize;
	[SerializeField] private int _areaYSize;
	[SerializeField] private int _areaZSize;

    [Header("Areas")]
	public List<Area> Areas = new();

	public List<ExpandMap> ExpandMaps = new();

	public int AreaXSize => _areaXSize;
	public int AreaYSize => _areaYSize;
	public int AreaZSize => _areaZSize;

	public void SetCopiedMap(MapManager manager)
    {
		_manager = manager;
        foreach (Area area in Areas)
        {
            area.SetNeighbouringAreas(area.GridSpot);
            _manager.Gen.GenerateMap(area, new Vector2Int(AreaXSize, AreaZSize));
        }
    }

	public void CreateArea(Vector2Int gridSpot)
	{
		Area area = new();
		area.SetNeighbouringAreas(gridSpot);
		CreateLayers(area, gridSpot);

		Areas.Add(area);
		_manager.Gen.GenerateMap(area, new Vector2Int(_areaXSize, _areaZSize));
	}

	public void CreateLayers(Area area, Vector2Int gridSpot)
    {
		for (int y = 0; y < AreaYSize; y++)
		{
			Layer layer = new();
			layer.LayerIndex = y;
			layer.CreateLayer(AreaXSize, AreaZSize, gridSpot);
			area.Layers.Add(layer);
		}
	}

	public void SetMap(int XSize, int YSize, int ZSize, MapManager manager)
    {
		_manager = manager;
		_areaXSize = XSize;
		_areaYSize = YSize;
		_areaZSize = ZSize;
    }
}
