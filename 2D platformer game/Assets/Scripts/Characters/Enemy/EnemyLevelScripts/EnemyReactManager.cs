using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReactManager : MonoBehaviour, INoiseHearable
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyMovementStateMachine _state;
    [SerializeField] private AStarPathFind _path;

    public void Init(Enemy enemy, EnemyMovementStateMachine state, AStarPathFind path)
    {
        _enemy = enemy;
        _state = state;
        _path = path;
    }
    public void OnNouseHeared(Vector3Int noiseStart)
    {
        _state.OnStateChange(_state.Search, _path.FindPath(_enemy.Data.CurrentPosition + Vector3Int.down, noiseStart));
    }
}
