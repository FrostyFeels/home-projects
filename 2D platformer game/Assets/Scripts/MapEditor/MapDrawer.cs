using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class MapDrawer : MonoBehaviour
{
    [SerializeField] private MapManager _manager;

    private TileStats _firstTile;
    private TileStats _lastTile;

    [SerializeField] private List<Tile> _tiles = new();
    private bool _filling = true;

    public void GetFirstTile()
    {
        if (_manager.Selector.CurrentTile != null)
        {
            _firstTile = _manager.Selector.CurrentTile;
            _lastTile = _firstTile;
            _tiles.Add(TileManager.Tiles[_firstTile.GridId]);
            _manager.Showcaser.DrawTiles(_tiles, _filling);
            _tiles.Clear();
        }
    }

    public void UpdateDrawMode(bool fill)
    {
        _filling = fill;
        _manager.Updater.UpdateMapHeight(TileManager.Tiles.Values, _filling);
    }

    public void OnFinish()
    {
        if(_firstTile != null)
        {
            Tile tile = TileManager.Tiles[_firstTile.GridId];
            tile.IsSelected = !tile.IsSelected;
        }
        
        _manager.Updater.UpdateTileStatus(_tiles, _filling);
        _manager.Updater.UpdateMapHeight(_tiles, _filling);
        _manager.Showcaser.ShowCaseMap(_tiles);
        _tiles.Clear();
    }


    //Collects all the tiles selected based on the build mode
    public void GetArea()
    {
        if (_firstTile == null || _manager.Selector.CurrentTile == null || _manager.Selector.CurrentTile == _lastTile)
        {
            return;
        }

        _manager.Showcaser.DrawTiles(_tiles, !_filling);
        _tiles.Clear();

        Vector2Int lenghtIndexes = DrawerHelper.OrderIndex(_firstTile.GridId.x, _manager.Selector.CurrentTile.GridId.x);
        Vector2Int widhtIndexes = DrawerHelper.OrderIndex(_firstTile.GridId.z, _manager.Selector.CurrentTile.GridId.z);

        Vector3Int start = new Vector3Int(lenghtIndexes.x, _firstTile.GridId.y, widhtIndexes.x);
        Vector3Int end = new Vector3Int(lenghtIndexes.y, _firstTile.GridId.y, widhtIndexes.y);

        _tiles = _manager.BuildState.ReceiveTiles(start, end, _filling);
        _manager.Showcaser.DrawTiles(_tiles, _filling);
        _lastTile = _manager.Selector.CurrentTile;
    }


}
