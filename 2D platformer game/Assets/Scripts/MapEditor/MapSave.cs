
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Events;

public class MapSave : MonoBehaviour
{
    public UnityAction<GameObject> onSave;

    private List<GameObject> tiles = new();
    [SerializeField] private MapManager _manager;
    public void SaveMap()
    {
        GameObject mapHolder = new();

        Map map = mapHolder.AddComponent<Map>();
        map.Areas = _manager.map.Areas;
        map.SetMap(_manager.map.AreaXSize, _manager.map.AreaYSize, _manager.map.AreaZSize, _manager);         

        onSave.Invoke(mapHolder);

        if(!Directory.Exists("Assets/Level Maps"))
        {
            AssetDatabase.CreateFolder("Assets", "Level Maps");
        }
        string localPath = "Assets/Level Maps/" + _manager.Gen.MapName + ".prefab";
        AssetDatabase.DeleteAsset(localPath);


        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        
        PrefabUtility.SaveAsPrefabAsset(mapHolder, localPath, out bool succes);
        
        Destroy(mapHolder);

        tiles.Clear();
    }
}
