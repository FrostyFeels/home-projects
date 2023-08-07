using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyMovementStateMachine : MonoBehaviour
{
    private AStarPathFind _aStarFind;
    private Enemy _enemy;

    public MovementState Patrol;
    public MovementState Chase = new EnemyChaseState();
    public MovementState Search = new EnemySearchState();
    public MovementState WalkBack = new EnemyWalkBackState();

    private int _tilesToMove;
    private float _moveTime;

    private MovementState _currentState;

    public void Init(AStarPathFind astar, MovementState state, Enemy enemy)
    {
        _enemy = enemy;
        _aStarFind = astar;

        _tilesToMove = 8;
        _moveTime = .05f;

        Patrol = state;
        Patrol.Init(enemy, _tilesToMove, _moveTime);
        Chase.Init(enemy, _tilesToMove, _moveTime);
        Search.Init(enemy, _tilesToMove, _moveTime);
        WalkBack.Init(enemy, _tilesToMove, _moveTime);

        _currentState = state;
        _currentState.SetPath(enemy.Data.PathIndexes);
    }

    public async Task Move()
    {
        await _currentState.Move();
    }

    public void OnStateChange(MovementState state, List<Vector3Int> path)
    {
        _currentState.OnFinish -= OnFinish;
        _currentState = state;
        state.OnFinish += OnFinish;
        _currentState.SetPath(path);
    }

    public void OnFinish()
    {
        if (_currentState == Search)
        {
            OnStateChange(WalkBack, _aStarFind.FindPath(_enemy.Data.CurrentPosition + Vector3Int.down, _enemy.Data.PathIndexes[Patrol.PathIndex]));
            return;
        }

        if (_currentState == WalkBack)
        {
            OnStateChange(Patrol, _enemy.Data.PathIndexes);
            return;
        }

    }

    public void OnDestroy()
    {
        _currentState.OnFinish -= OnFinish;
    }
}
