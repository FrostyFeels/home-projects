using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class EnemyPatrolStateLoop : MovementState
{
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
    public async override Task Move()
    {
        CheckValidaty();
        SetNextTiles();
        await MovePlayerAsync();
    }

    private void CheckValidaty()
    {
        if (PathIndex + 1 > Path.Count - 1)
        {
            PathIndex = 0;
        }
    }

    private void SetNextTiles()
    {
        StartPosition = Path[PathIndex] + Vector3Int.up;
        EndPosition = Path[PathIndex + 1] + Vector3Int.up;
        PathIndex++;
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
