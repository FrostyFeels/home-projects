using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesManager
{
    public static Mesh GetMesh(string name)
    {
        GameObject tile = Resources.Load("Prefabs/" + name) as GameObject;
        return tile.GetComponent<MeshFilter>().sharedMesh;

    }

    public static void SetMeshes(List<Tile> tiles, string name)
    {
        GameObject tileGo = Resources.Load("Prefabs/" + name) as GameObject;
        Mesh mesh = tileGo.GetComponent<MeshFilter>().sharedMesh;

        foreach(Tile tile in tiles)
        {
            tile.filter.mesh = mesh;
        }     
    }

    public static void SetMaterials(List<Tile> tiles, string name)
    {
        Material material = Resources.Load("Materials/" + name) as Material;
        foreach (Tile tile in tiles)
        {
            tile.Render.material = material;
        }
    }

    public static Material GetMaterial(string name)
    {
        return Resources.Load("Materials/" + name) as Material;

    }
}
