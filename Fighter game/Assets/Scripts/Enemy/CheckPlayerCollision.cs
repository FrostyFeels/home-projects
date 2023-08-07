using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerCollision : MonoBehaviour
{
    [SerializeField] private float dmg;


    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponentInParent<PlayerHealth>();
            health.TakeDamage(dmg);
            health.OnHit(transform.position);
        }
    }
}

