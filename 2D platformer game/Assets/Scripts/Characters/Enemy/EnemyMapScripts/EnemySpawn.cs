using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public List<Enemy> Enemies;
    [SerializeField] private MapManager _mManager;
    [SerializeField] private CharacterManager _cManager;

    [SerializeField] private Button _addBtn;
    [SerializeField] private Button _finishBtn;

    [Header("Prefab")]
    [SerializeField] private GameObject _enemyPrefab;

    private void Start()
    {
        _addBtn.onClick.AddListener(OnSelect);
        enabled = false;
    }

    public void OnStateChange(bool isEnabled)
    {
        _addBtn.gameObject.SetActive(isEnabled);
    }

    public void SpawnEnemy()
    {
        if (_mManager.Selector.CurrentTile == null) return;

        Vector3Int positon = _mManager.Selector.CurrentTile.GridId + Vector3Int.up;

        if(EnemySpawnManager.SpawnPoints.Contains(positon)) return;

        GameObject enemy = Instantiate(_enemyPrefab);

        enemy.transform.position = positon;

        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemy.AddComponent<EnemyInfoCollider>().Init(enemyScript, _cManager.Stats, _cManager, _mManager);
        enemyScript.Data.StartPosition = positon;

        EnemySpawnManager.SpawnPoints.Add(enemyScript.Data.StartPosition);
        Enemies.Add(enemyScript);
    }

    private void OnSelect()
    {
        if (_cManager.PSpawn.enabled) return;
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
}
