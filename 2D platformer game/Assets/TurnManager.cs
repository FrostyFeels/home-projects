using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private List<EnemyMovementStateMachine> _enemiesMove = new();
    [SerializeField] private AStarPathFind _aStar;

    public void AddEnemy(EnemyMovementStateMachine move, Enemy enemy)
    {
        _enemiesMove.Add(move);
        if(enemy.Data.loop)
        {
            move.Init(_aStar, new EnemyPatrolStateLoop(), enemy);
        }
        else
        {
            move.Init(_aStar, new EnemyPatrolState(), enemy);
        }
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            DoTurn();
        }
    }

    private async void DoTurn()
    {
        foreach (EnemyMovementStateMachine enemy in _enemiesMove)
        {
            await enemy.Move();
        }
    }
}
