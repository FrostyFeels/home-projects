using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class EnemyPatrolState : MovementState
{
    private bool _forward = false;

    public override void Init(Enemy enemy, int tilesToMove, float timeToMove)
    {
        TheEnemy = enemy;
        TilesToMove = tilesToMove;
        TimeToMove = timeToMove;
    }

    public override void SetPath(List<Vector3Int> path)
    {
        Path = path;
    }

    public override async Task Move()
    {
        CheckValidaty();
        SetNextTiles();
        await MovePlayerAsync();
    }

    private void CheckValidaty()
    {
        if (PathIndex >= Path.Count - 1)
        {
            _forward = false;
        }

        if(PathIndex <= 0)
        {
            _forward = true;
        }
    }

    private void SetNextTiles()
    {
        StartPosition = Path[PathIndex] + Vector3Int.up;
        if (_forward)
        {
            EndPosition = Path[PathIndex + 1] + Vector3Int.up;
            PathIndex++;
        }
        else
        {
            EndPosition = Path[PathIndex - 1] + Vector3Int.up;
            PathIndex--;
        }   
    }

    async Task MovePlayerAsync()
    {
        while (MovedTime < TimeToMove)
        {
            TheEnemy.transform.position = Vector3.Lerp(StartPosition, EndPosition, MovedTime / TimeToMove);
            MovedTime += Time.deltaTime;
         
            await Task.Yield();
        }
        MovedTime = 0;
        TheEnemy.transform.position = EndPosition;
        TheEnemy.Data.CurrentPosition = EndPosition;
        MovedTiles++;

        if (MovedTiles < TilesToMove)
        {
            await Move();
        }
        else
        {
            MovedTiles = 0; 
        }
    }
}
