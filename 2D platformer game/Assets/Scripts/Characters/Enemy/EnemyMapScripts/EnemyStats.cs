using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _classText;
    [SerializeField] private Button _pathBtn;
    [SerializeField] private Button _positionBtn;
    [SerializeField] private Button _killBtn;
    [SerializeField] private Button _exitBtn;

    [SerializeField] private EnemyPathMaker _pathMaker;
    [SerializeField] private EnemyPositionSetter _positionSetter;
    [SerializeField] private EnemySpawn _spawn;

    public string[] Names = { "Rose", "Fish", "Sylvee", "Canis" };
    public string[] ClassNames = { "Tank", "test dummy", "goblin", "goddess" };

    private void Start()
    {
        _exitBtn.onClick.AddListener(Exit);
    }

    public void SetValue(string name, string className, Enemy enemy)
    {
        _nameText.text = $"Name: {name}";
        _classText.text = $"Class: {className}";

        RemoveListeners();

        _pathBtn.onClick.AddListener(() => _pathMaker.SetEnemy(enemy));
        _positionBtn.onClick.AddListener(() => _positionSetter.SetEnemy(enemy));
        _killBtn.onClick.AddListener(() => DestroyEnemy(enemy));

        gameObject.SetActive(true);
    }

    private void DestroyEnemy(Enemy enemy)
    {
        EnemySpawnManager.SpawnPoints.Remove(enemy.Data.StartPosition);
        _spawn.Enemies.Remove(enemy);
        Destroy(enemy.gameObject);
        gameObject.SetActive(false);
        RemoveListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
        _exitBtn.onClick.RemoveAllListeners();
    }

    private void Exit()
    {
        gameObject.SetActive(false);
        RemoveListeners();
    }

    private void RemoveListeners()
    {
        _pathBtn.onClick.RemoveAllListeners();
        _positionBtn.onClick.RemoveAllListeners();
        _killBtn.onClick.RemoveAllListeners();
    }
}
