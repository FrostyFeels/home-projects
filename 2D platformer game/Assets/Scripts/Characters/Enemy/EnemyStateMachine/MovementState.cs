using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public abstract class MovementState
{
    protected Enemy TheEnemy;

    protected List<Vector3Int> Path = new();
    
    protected int MovedTiles;
    protected int TilesToMove;

    protected float MovedTime;
    protected float TimeToMove;
    
    protected Vector3Int StartPosition;
    protected Vector3Int EndPosition;

    public int PathIndex;

    public UnityAction OnFinish;

    public abstract void Init(Enemy enemy, int tilesToMove, float timeToMove);
    public abstract void SetPath(List<Vector3Int> path);
    public abstract Task Move();
}
