using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMovement : EnemyMovement
{
    public bool hasShot;
    public void Update()
    {
        if (enemyState == EnemyState.STUNNED || enemyState == EnemyState.ATTACK)
            return;

        CombatCheck();
        if (!inCombat)
            return;


        if (enemyState != EnemyState.MOVING)
            return;

        PlayerToEnemyDirection();

        if (Vector2.Distance(transform.position, _Target.transform.position) < attackRadius)
        {
            SwapStates("Attacking");
        }


 
    }

    public void FixedUpdate()
    {
        if (enemyState == EnemyState.MOVING)
            rb.velocity = _dir * speed * Time.fixedDeltaTime;
    }
}
