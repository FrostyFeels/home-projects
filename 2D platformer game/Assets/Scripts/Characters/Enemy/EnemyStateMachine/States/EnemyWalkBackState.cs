using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyWalkBackState : MovementState
{
    public override void Init(Enemy enemy, int tilesToMove, float timeToMove)
    {
        TheEnemy = enemy;
        TilesToMove = tilesToMove;
        TimeToMove = timeToMove;
    }

    public override void SetPath(List<Vector3Int> path)
    {
        PathIndex = 0;
        Path = path;

    }
    public override async Task Move()
    {
        SetNextTiles();
        await MovePlayerAsync();
        return;
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

        if (TheEnemy.Data.CurrentPosition == Path[^1] + Vector3Int.up)
        {
            MovedTiles = 0;
            OnFinish?.Invoke();
            return;
        }
        else
        {
            if (Path.Count - 1 == PathIndex)
            {
                Debug.Log(TheEnemy.Data.CurrentPosition + " " + Path[^1] + Vector3Int.up);
            }
        }

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
