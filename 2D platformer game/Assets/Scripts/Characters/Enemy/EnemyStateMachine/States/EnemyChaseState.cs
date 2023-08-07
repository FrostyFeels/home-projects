using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class EnemyChaseState : MovementState
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

    public override async Task Move()
    {
        return;
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
