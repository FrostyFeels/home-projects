using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("scripts")]
    [SerializeField] private TurnManager _turnManager;
    [SerializeField] private Map _map;
    [SerializeField] private EnemySaver _enemy;
    [SerializeField] private PlayerSaver _player;

    [Header("prefab")]
    [SerializeField] private TileStats _tilePrefab;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Player _playerPrefab;

    [SerializeField] private GameObject _enemyHolder;

    [SerializeField] private AStarGrid _aStarGrid;
    [SerializeField] private AStarPathFind _aStarPathFind;

    private void Awake()
    {
        GenerateMap();
        GenerateEnemy();
        GeneratePlayer();
        _aStarGrid.CreateGrid();

        StaticBatchingUtility.Combine(_map.gameObject);
    }

    private void GenerateEnemy()
    {
        foreach (Enemy.EnemyData data in _enemy.Enemies)
        {
            Enemy enemy = Instantiate(_enemyPrefab);
            enemy.Data = data;
            enemy.transform.parent = _enemyHolder.transform;
            enemy.transform.position = enemy.Data.StartPosition;
            enemy.Data.CurrentPosition = enemy.Data.StartPosition;
            EnemyMovementStateMachine move = enemy.gameObject.AddComponent<EnemyMovementStateMachine>();
            enemy.gameObject.AddComponent<EnemyReactManager>().Init(enemy, move, _aStarPathFind);
            _turnManager.AddEnemy(move, enemy);
        }
    }

    private void GeneratePlayer()
    {
        foreach (Player.PlayerData data in _player.PlayerDatas)
        {
            Player player = Instantiate(_playerPrefab);
            player.Data = data;
            player.transform.parent = _enemyHolder.transform;
            player.transform.position = player.Data.SpawnPosition;
        }
    }

    private void GenerateMap()
    {
        foreach (Area area in _map.Areas) 
        {
            foreach (Layer layer in area.Layers)
            {
                foreach(Tile tile in layer.Tiles)
                {
                    if(tile.IsSelected)
                    {
                        SetTileStats(tile);
                    }
                }
            }
        }
    }

    private void SetTileStats(Tile tile)
    {
        TileStats newTile = Instantiate(_tilePrefab, _map.transform);
        newTile.GridId = tile.Pos;
        newTile.transform.position = tile.Pos;

        tile.neighbours = 0;
        tile.SetTileNeighbours();
        tile.GetLowerNeighbour();
        tile.GetUpperNeighbour();
        tile.Render = newTile.gameObject.GetComponent<Renderer>();
        tile.filter = newTile.gameObject.GetComponent<MeshFilter>();
        tile.Obj = newTile.gameObject;

        TileManager.Tiles.Add(tile.Pos, tile);
    }
}
