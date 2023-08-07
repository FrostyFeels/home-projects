using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AttackCollisionCheck : MonoBehaviour
{


    [SerializeField] private Attack attack;
    [SerializeField] private int comboID;

    [SerializeField] private float bloodEffectTime;
    [SerializeField] private float stunTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("LMAO LOSER");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

                
            EnemyMovement enemymov = collision.GetComponent<EnemyMovement>();
            enemymov.OnStunned(stunTime);
            attack.PushEnemy(collision.GetComponent<Rigidbody2D>());

            Vector3 direction = (collision.gameObject.transform.position - transform.position);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (comboID == 2)
                angle *= -1;

            GameObject effectObject = Instantiate(attack.bloodprefab, collision.gameObject.transform.position, Quaternion.Euler(0, 0, 60 + angle), collision.gameObject.transform);
            Destroy(effectObject, bloodEffectTime);
            



        }
    }





}
