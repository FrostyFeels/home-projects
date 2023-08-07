using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData Data;


    [Serializable]
    public class PlayerData
    {
        public Vector3Int SpawnPosition;
    }
}
