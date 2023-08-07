using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyInfoCollider : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private CharacterManager _cManager;
    [SerializeField] private MapManager _mManager;
    [SerializeField] private EnemyStats _stats;

    private string _name;
    private string _className;

    public void Init(Enemy enemy, EnemyStats stats, CharacterManager cManager, MapManager mManager)
    {
        _enemy = enemy;
        _stats = stats;
        _cManager = cManager;
        _mManager = mManager;
        _name = stats.Names[Random.Range(0, stats.Names.Length)];
        _className = stats.ClassNames[Random.Range(0, stats.ClassNames.Length)];
    }

    private void OnMouseDown()
    {
        if (_cManager.ESpawn.enabled
            || _cManager.Position.enabled
            || _cManager.Path.enabled
            || _cManager.PSpawn.enabled
            || IsMouserOverUI()
            || !_mManager.MapState.CheckState(_mManager.MapState._characterState)) return;
        _stats.SetValue(_name, _className, _enemy);
    }

    private bool IsMouserOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

}
