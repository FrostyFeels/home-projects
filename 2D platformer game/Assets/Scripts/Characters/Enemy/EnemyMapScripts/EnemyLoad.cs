using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLoad : MonoBehaviour
{
    [Header("scripts")]
    [SerializeField] private MapManager _mManager;
    [SerializeField] private CharacterManager _cManager;

    [Header("prefab")]
    [SerializeField] private Enemy _enemyPrefab;


    private void Start()
    {
        _mManager.Load.onLoad += LoadEnemies;
    }

    private void LoadEnemies(GameObject obj)
    {
        EnemySaver enemySaver = obj.GetComponent<EnemySaver>();

        if (enemySaver == null) return;
        foreach (Enemy.EnemyData data in enemySaver.Enemies)
        {
            Enemy enemy = Instantiate(_enemyPrefab);
            enemy.Data = data;
            enemy.transform.position = data.StartPosition;
            enemy.gameObject.AddComponent<EnemyInfoCollider>().Init(enemy, _cManager.Stats, _cManager, _mManager);
            _cManager.ESpawn.Enemies.Add(enemy);
            EnemySpawnManager.SpawnPoints.Add(data.StartPosition);
        }
    }

    private void OnDestroy()
    {
       _mManager.Load.onLoad -= LoadEnemies;
    }
}
