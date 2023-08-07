using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Manager
{
    public EnemySpawn ESpawn;
    public EnemyPathMaker Path;
    public EnemyPositionSetter Position;
    public EnemyStats Stats;

    public PlayerSpawner PSpawn;
}
