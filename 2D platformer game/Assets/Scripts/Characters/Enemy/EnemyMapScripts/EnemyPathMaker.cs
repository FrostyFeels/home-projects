using System.Collections.Generic;
using System.Linq;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyPathMaker : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private CharacterManager _manager;
    [SerializeField] private TileSelector _selector;

    [SerializeField] private Button _resetBtn;
    [SerializeField] private Button _finishBtn;
    [SerializeField] private Button _cancelBtn;
    [SerializeField] private Button _pathTypeBtn;

    [SerializeField] private GameObject _confirmObj;

    [SerializeField] private ConfirmButton _confirm;

    private Enemy _enemy;

    private Material _selectedMat;
    private Material _currentMat;
    private Material _lastMat;
    private Material _firstMat;

    public Vector3Int _firstTile;
    public Vector3Int _currentTile;
    public Vector3Int _lastTile;

    private Vector3Int _emptyValue = new Vector3Int(-999, -999, -999);

    public List<Tile> _tiles = new();

    private bool loop = false;

    private void Start()
    {
        _pathTypeBtn.onClick.AddListener(ChangePathType);
        _selectedMat = ResourcesManager.GetMaterial("SelectedTilesMat");
        _currentMat = ResourcesManager.GetMaterial("CurrentTileMat");
        _lastMat = ResourcesManager.GetMaterial("LastTileMat");
        _firstMat = ResourcesManager.GetMaterial("FirsTileMat");
        enabled = false;
    }
    public void SetEnemy(Enemy enemy)
    {
        _manager.Stats.gameObject.SetActive(false);
        _enemy = enemy;

        if(_enemy.Data.PathIndexes.Count > 1)
        {
            SetPath();

        }
        else
        {
            _currentTile = _enemy.Data.StartPosition + Vector3Int.down;
            _firstTile = _currentTile;
            _tiles.Add(TileManager.Tiles[_currentTile]);
        }

        _resetBtn.gameObject.SetActive(true);
        _finishBtn.gameObject.SetActive(true);
        _cancelBtn.gameObject.SetActive(true);
        _pathTypeBtn.gameObject.SetActive(true);

        _resetBtn.onClick.AddListener(OnReset);
        _finishBtn.onClick.AddListener(Finish);
        _cancelBtn.onClick.AddListener(OnCancel);
        

        enabled = true;

        
        TileManager.Tiles[_firstTile].Render.material = _firstMat;
        enemy.gameObject.SetActive(false);
    }

    public void SetPath()
    {
        foreach (Vector3Int pathTile in _enemy.Data.PathIndexes)
        {
            Tile tile = TileManager.Tiles[pathTile];
            _tiles.Add(tile);        
        }

        DrawTiles(true);
        _firstTile = _enemy.Data.PathIndexes[0];
        _currentTile = _enemy.Data.PathIndexes.Last();
        
    }

    public void WritePath()
    {
        if (_selector.CurrentTile == null) return;
        Vector3Int currentId = _selector.CurrentTile.GridId;
        if (currentId == _currentTile) return;

        int distance = Mathf.Abs(currentId.x - _currentTile.x) + Mathf.Abs(currentId.z - _currentTile.z);


        if (distance > 1) return;

        DrawTiles(false);

        if (currentId == _lastTile && _tiles.Count > 1)
        {
            int lastTileIndex = _tiles.LastIndexOf(_tiles[^1]);
            _tiles.RemoveAt(lastTileIndex);
            _currentTile = _lastTile;

            if(_tiles.Count > 1)
            {
                _lastTile = _tiles[^2].Pos;
            }
            else
            {
                _lastTile = _emptyValue;
            }
            
        }
        else
        {
            _tiles.Add(TileManager.Tiles[currentId]);
            _lastTile = _currentTile;
            _currentTile = currentId;
        }

        DrawTiles(true);
        TileManager.Tiles[_currentTile].Render.material = _currentMat;

        if (_lastTile != _emptyValue)
        {
            TileManager.Tiles[_lastTile].Render.material = _lastMat;
        }

        TileManager.Tiles[_firstTile].Render.material = _firstMat;
    }

    private void DrawTiles(bool enabled)
    {
        foreach (Tile tile in _tiles)
        {
            if(enabled)
            {
                tile.Render.material = _selectedMat;
            }
            else
            {
                tile.Render.material = tile.defaultMaterial;
            }
        }
    }

    private void ChangePathType()
    {
        loop = !loop;
        if(loop)
        {
            _pathTypeBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Loop Mode";
        }
        else
        {
            _pathTypeBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Patrol Mode";
        }
        
    }

    private void OnCancel()
    {
        _confirmObj.SetActive(true);
        _confirm.SetListener(Cancel);
    }

    private void Cancel()
    {
        DrawTiles(false);
        _tiles.Clear();

        _pathTypeBtn.gameObject.SetActive(false);
        RemoveListeners();
        _enemy.gameObject.SetActive(true);
        _enemy = null;
        enabled = false;
    }

    private void OnReset()
    {
        DrawTiles(false);
        _tiles.Clear();

        _currentTile = _enemy.Data.StartPosition + Vector3Int.down;
        _tiles.Add(TileManager.Tiles[_currentTile]);
        _tiles[0].Render.material = _firstMat;
        _firstTile = _currentTile;
    }

    private void Finish()
    {
        if(loop)
        {
            if (!CheckIfLoop())
            {
                return;
            }
        }

        DrawTiles(false);
        _enemy.Data.PathIndexes.Clear();

        foreach (Tile tile in _tiles)
        {
            _enemy.Data.PathIndexes.Add(tile.Pos);
        }


            _enemy.Data.loop = loop;
   

        _tiles.Clear();
        _enemy.gameObject.SetActive(true);
        _enemy = null;

        _pathTypeBtn.gameObject.SetActive(false);
        RemoveListeners();

        enabled = false;
    }

    private bool CheckIfLoop()
    {
        return _tiles[^1] == _tiles[0];
    }

    private void RemoveListeners()
    {
        _resetBtn.onClick.RemoveListener(OnReset);
        _finishBtn.onClick.RemoveListener(Finish);
        _cancelBtn.onClick.RemoveListener(OnCancel);

        _resetBtn.gameObject.SetActive(false);
        _finishBtn.gameObject.SetActive(false);
        _cancelBtn.gameObject.SetActive(false);
    }
}
