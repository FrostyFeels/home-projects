using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Enemy : MonoBehaviour
{
    public EnemyData Data;

    [Serializable]
    public class EnemyData
    {
        public bool loop;
        public List<Vector3Int> PathIndexes = new();
        public Vector3Int StartPosition;
        public Vector3Int CurrentPosition;
    }
}
