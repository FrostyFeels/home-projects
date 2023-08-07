using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnStunned : MonoBehaviour
{
    public EnemyMovement ai;
    public float stunDuration;
    public float currentStunTime;

    public void Update()
    {
        if(stunDuration > currentStunTime)
        {
            currentStunTime += Time.deltaTime;      
        }

        if(currentStunTime >= stunDuration)
        {
            ai.enemyState = EnemyMovement.EnemyState.MOVING;
            Debug.Log("Ran");
            this.enabled = false;      
        }       
    }
}
