using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
	[Header("Current Selected")]
	public TileStats CurrentTile;
    public bool GetSurroundingTiles = true;

    [SerializeField] private MapShowCaser _mapShowCaser;
    [SerializeField] private int _areaToSelect;

    [Header("LayerMask")]
    [SerializeField] private LayerMask _selectable;

    private Dictionary<Tile, int> _surroundesTiles = new();
	
    private void Update()
    {
		ReceiveSelectedTile();
    }

    public void ReceiveSelectedTile()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, _selectable))
		{
			if (hit.collider.TryGetComponent(out TileStats tileStats))
			{
				if (CurrentTile != null && CurrentTile == tileStats)
				{
					return;
				}

				CurrentTile = tileStats;

				if (GetSurroundingTiles)
				{
					EmptySelectedTiles();
					_surroundesTiles.Clear();

					GetSurroundedTiles();
                    _mapShowCaser.ShowCaseSelectedTiles(_surroundesTiles);
                }
				return;
			}
		}

		CurrentTile = null;		
	}

	public void EmptySelectedTiles()
    {
		_mapShowCaser.EmptySelectedTiles(_surroundesTiles.Keys);
	}

	private void GetSurroundedTiles()
    {
		int startX = CurrentTile.GridId.x - (_areaToSelect / 2);
		int startZ = CurrentTile.GridId.z - (_areaToSelect / 2);

		for (int z = startZ; z < startZ + _areaToSelect; z++)
        {
            for (int x = startX; x < startX + _areaToSelect; x++)
            {
				Vector3Int pos = new Vector3Int(x, CurrentTile.GridId.y, z);
				int maxDistance = GetFarthestDistance(pos, CurrentTile.GridId);
				if(TileManager.Tiles.TryGetValue(pos, out Tile tile))
                {
					if(!tile.IsEdgeTile)
					{
                        _surroundesTiles.TryAdd(tile, maxDistance);
                    }				
                }			
            }
        }
    }

	private int GetFarthestDistance(Vector3Int start, Vector3Int end)
    {
		int xDistance = Mathf.Abs(start.x - end.x);
		int zDistance = Mathf.Abs(start.z - end.z);

		return xDistance > zDistance ? xDistance : zDistance;
    }
}
