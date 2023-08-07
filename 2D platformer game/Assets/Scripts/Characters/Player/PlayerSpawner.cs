using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    [Header("scripts")]
    [SerializeField] private MapManager _manager;
    [SerializeField] private EnemySpawn _spawn;
    [SerializeField] private Player _playerPrefab;

    [Header("buttons")]
    [SerializeField] private Button _addBtn;
    [SerializeField] private Button _finishBtn;

    private int _characterSpawnIndex = 1;

    public Dictionary<int, Player> _players = new();

    private void Start()
    {
        _addBtn.onClick.AddListener(OnSelect);
        enabled = false;
    }

    public void OnStateChange(bool isEnabled)
    {
        _addBtn.gameObject.SetActive(isEnabled);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            _characterSpawnIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _characterSpawnIndex = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _characterSpawnIndex = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _characterSpawnIndex = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _characterSpawnIndex = 5;
        }

        if(Input.GetMouseButtonDown(0))
        {
            SpawnPlayer();
        }
    }

    private void OnSelect()
    {
        if (_spawn.enabled) return;
        _finishBtn.onClick.RemoveAllListeners();
        _finishBtn.gameObject.SetActive(true);
        _finishBtn.onClick.AddListener(OnFinish);

        _addBtn.gameObject.SetActive(false);
        enabled = true;
    }

    private void OnFinish()
    {
        _finishBtn.onClick.RemoveAllListeners();
        _finishBtn.gameObject.SetActive(false);
        _addBtn.gameObject.SetActive(true);
        enabled = false;
    }

    private void SpawnPlayer()
    {
        if (_manager.Selector.CurrentTile == null) return;
        Vector3Int startPosition = _manager.Selector.CurrentTile.GridId + Vector3Int.up;

        if (EnemySpawnManager.SpawnPoints.Contains(startPosition)) return;

        if (!_players.ContainsKey(_characterSpawnIndex))
        {
            Player player = Instantiate(_playerPrefab);
            player.Data.SpawnPosition = startPosition;
            player.transform.position = startPosition;
            _players[_characterSpawnIndex] = player;
            EnemySpawnManager.SpawnPoints.Add(startPosition);
            return;
        }
        
        Player aPlayer = _players[_characterSpawnIndex];
        
        if(aPlayer.Data.SpawnPosition != startPosition)
        {
            EnemySpawnManager.SpawnPoints.Remove(aPlayer.Data.SpawnPosition);
        }

        aPlayer.Data.SpawnPosition = startPosition;
        aPlayer.transform.position = startPosition;
        EnemySpawnManager.SpawnPoints.Add(startPosition);
    } 
}
