using UnityEngine;

public class ExpandMap : MonoBehaviour
{
	private Map _map;
	private Vector2Int _gridSpot;
	private MapGenerator mapGen;

	public void Init(Map map, Vector2Int gridSpot, MapGenerator gen)
	{
		_map = map;
		_gridSpot = gridSpot;
		mapGen = gen;

		mapGen.OnGenerateMap += CheckValidity;
	}

	private void OnMouseDown()
	{
		_map.CreateArea(_gridSpot);
		Destroy(gameObject);
	}

	public void CheckValidity()
	{
		if(AreaManager.Areas.ContainsKey(_gridSpot))
		{
			Destroy(gameObject);
		}
	}

    private void OnDestroy()
    {
		mapGen.OnGenerateMap -= CheckValidity;
		_map.ExpandMaps.Remove(this);
    }
}


