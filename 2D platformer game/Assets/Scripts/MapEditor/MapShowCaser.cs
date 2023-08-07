using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShowCaser : MonoBehaviour
{
    private Dictionary<int, Material> selectedMaterials = new();

    private Material _filled;
    private Material _unFilled100;
    private Material _unFilled66;
    private Material _unFilled33;
    private Material _unFilled;

    private Mesh _build;
    private Mesh _buildable;

    private void Awake()
    {
        _filled = ResourcesManager.GetMaterial("Filled");
        _unFilled100 = ResourcesManager.GetMaterial("UnFilled 100");
        _unFilled66 = ResourcesManager.GetMaterial("UnFilled 66");
        _unFilled33 = ResourcesManager.GetMaterial("UnFilled 33");
        _unFilled = ResourcesManager.GetMaterial("Unfilled");

        _build = ResourcesManager.GetMesh("Build");
        _buildable = ResourcesManager.GetMesh("Buildable");

        selectedMaterials.Add(0, _unFilled100);
        selectedMaterials.Add(1, _unFilled66);
        selectedMaterials.Add(2, _unFilled33);
    }
    public void ShowCaseMap(ICollection<Tile> tiles)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.IsSelected)
            {
                tile.defaultMaterial = _filled;
                tile.filter.sharedMesh = _build;
            }
            else
            {
                if (tile.IsEdgeTile)
                {
                    tile.defaultMaterial = _unFilled100;
                }
                else
                {
                    tile.defaultMaterial = _unFilled;
                }

                tile.filter.sharedMesh = _buildable;
            }
            tile.Render.material = tile.defaultMaterial;
        }
    }

    public void EnableEdgeTiles(bool enable)
    {
        foreach (Tile tile in TileManager.EdgeTiles)
        {
            if (!tile.IsSelected)
            {
                if (enable)
                {
                    tile.Render.material = _unFilled100;
                }
                else
                {
                    tile.Render.material = _unFilled;
                }
            }
        }
    }

    public void ShowCaseSelectedTiles(Dictionary<Tile, int> tiles) 
    {
        foreach ((Tile tile, int distance) in tiles)
        {
            if(tile.filter.sharedMesh == _buildable)
            {
                tile.Render.material = selectedMaterials[distance];
            }        
        }
    }

    public void EmptySelectedTiles(ICollection<Tile> tiles)
    {
        foreach (Tile tile in tiles)
        {
            if (!tile.IsSelected)
            {
                tile.Render.material = tile.defaultMaterial;
            }
        }
    }

    public void DrawTiles(List<Tile> tiles, bool enable)
    {
        foreach (Tile tile in tiles)
        {
            if (enable)
            {
                tile.filter.sharedMesh = _build;
                tile.Render.material = _filled;

                if(!tile.IsEdgeTile)
                {
                    tile.defaultMaterial = _filled;
                }
            }
            else
            {
                if(!tile.IsEdgeTile)
                {
                    tile.defaultMaterial = _unFilled;
                }
                tile.filter.sharedMesh = _buildable;
                tile.Render.material = tile.defaultMaterial;
            }
        }
    }
}
