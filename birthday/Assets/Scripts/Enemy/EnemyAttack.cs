using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public GameObject weaponHolder;
    public GameObject player;

    public bool inAttack;


    public EnemyMovement move;


    public void RotateWeapon()
    {
        float angle = CalculateFromPlayerToEnemy();
        var offset = 90f;

        weaponHolder.transform.rotation = Quaternion.Euler(Vector3.forward * (angle - offset));
    }

    public float CalculateFromPlayerToEnemy()
    {
        Vector2 pos = player.transform.position;
        Vector2 direction = (pos - (Vector2)transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        return angle;
    }

    public virtual void CancelAttack()
    {

    }
}
