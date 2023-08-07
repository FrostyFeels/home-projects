using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySave : MonoBehaviour
{
    [Header("scripts")]
    [SerializeField] private MapManager _Mmanager;
    [SerializeField] private CharacterManager _eManager;

    private void Start()
    {
        _Mmanager.Save.onSave += SaveEnemies;
    }

    private void SaveEnemies(GameObject mapholder)
    {
        EnemySaver saver = mapholder.AddComponent<EnemySaver>();
        foreach (Enemy enemy in _eManager.ESpawn.Enemies)
        {
            saver.Enemies.Add(enemy.Data);
        }

    }

    private void OnDestroy()
    {
        _Mmanager.Save.onSave -= SaveEnemies;
    }
}
