using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : EnemyAttack
{
    [SerializeField] private GameObject VFXEffect;
    [SerializeField] private float timeTillLocked, timeTillAttack, timeOfAttack;

    public void OnEnable()
    {
        StartCoroutine("Swing");
    }

    void Update()
    {
        if(!inAttack)
            RotateWeapon();     
    }

    public IEnumerator Swing()
    {
        yield return new WaitForSeconds(timeTillLocked);
        inAttack = true;
        yield return new WaitForSeconds(timeTillAttack);
        VFXEffect.SetActive(true);
        yield return new WaitForSeconds(timeOfAttack);
        VFXEffect.SetActive(false);
        inAttack = false;
        move.SwapStates("Moving");
        this.enabled = false;
        
    }

   
    public override void CancelAttack()
    {
        StopCoroutine("Swing");
        VFXEffect.SetActive(false);
        inAttack = false;
        this.enabled = false;
    }
}
