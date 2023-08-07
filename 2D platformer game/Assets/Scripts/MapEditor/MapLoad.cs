using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class MapLoad : MonoBehaviour
{
    public UnityAction<GameObject> onLoad;

    [SerializeField] private MapManager _manager;
    [SerializeField] private LoadMapInfoButton _button;

    [SerializeField] private GameObject _mapOptionSelector;
    [SerializeField] private GameObject _mapUISelector;
    [SerializeField] private GameObject _mapStateUI;

    [SerializeField] private Button _newMapButton;
    [SerializeField] private Button _loadMapButton;

    private string[] _guidPaths;

    //Collect all the paths from Unity
    //Set the buttons for clicking
    private void Start()
    {
        _guidPaths = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Level Maps" });

        if(_guidPaths.Length == 0)
        {
            LoadNewMap();
        }
        else
        {
            _newMapButton.onClick.AddListener(LoadNewMap);
            _loadMapButton.onClick.AddListener(SelectMaps);
        }
    }
    public void OpenMapOptions()
    {
        _mapOptionSelector.SetActive(true);
    }

    public void SelectMaps()
    {
        _mapOptionSelector.SetActive(false);
        GetAllMaps();
        _mapUISelector.SetActive(true);
    }

    private void GetAllMaps()
    {
        string location = "Assets/Level Maps";
        _guidPaths = AssetDatabase.FindAssets("t:prefab", new string[] { location });
            
        foreach (string guidPath in _guidPaths)
        {
            string path = AssetDatabase.GUIDToAssetPath(guidPath);
            string mapName = AssetDatabase.LoadAssetAtPath<GameObject>(path).name;
            LoadMapInfoButton mapInfo = Instantiate(_button);


            mapInfo.GetComponentInChildren<TextMeshProUGUI>().text = mapName;

            mapInfo.transform.SetParent(_mapUISelector.transform);
            mapInfo.SetButton(path, this);
        }     
    }

    public void LoadMap(string assetPath)
    {
        GameObject mapHolder = new GameObject("MapHolder");
        GameObject mapObj = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

        Map loadedMap = mapObj.GetComponent<Map>();
        Map newMap = mapHolder.AddComponent<Map>();

        newMap.Areas =  loadedMap.Areas;
        newMap.SetMap(loadedMap.AreaXSize, loadedMap.AreaYSize, loadedMap.AreaZSize, _manager);

        _manager.map = newMap;
        _manager.Gen.MapHolder = mapHolder;
     
        _manager.map.SetCopiedMap(_manager);

        _mapUISelector.SetActive(false);
        _mapStateUI.SetActive(true);

        onLoad?.Invoke(mapObj);

        EditorUtility.UnloadUnusedAssetsImmediate();

        List<Manager> manager = new() { _manager };
        _manager.MapState.OnStateChange(_manager.MapState._expandMapState, manager);
    }

    public void LoadNewMap()
    {
        _mapOptionSelector.SetActive(false);
        GameObject mapHolder = new GameObject();
        mapHolder.name = "MapHolder";

        Map map = mapHolder.AddComponent<Map>();
        _manager.map = map;
        _manager.Gen.MapHolder = mapHolder;

        Vector3Int mapSize = new Vector3Int(10, 3, 10);
        map.SetMap(mapSize.x, mapSize.y, mapSize.z, _manager);
        map.CreateArea(new Vector2Int(0, 0));
        _mapStateUI.SetActive(true);

        List<Manager> manager = new() { _manager };
        _manager.MapState.OnStateChange(_manager.MapState._expandMapState, manager);
    }

    public void finishedLoading()
    {
        //onLoad?.Invoke();
        

    }
}
