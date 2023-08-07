using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed;
    public float dmg;
    public Vector2 dir;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 5f);
    }
    private void FixedUpdate()
    {
        rb.velocity = dir * speed * Time.fixedDeltaTime; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth health = collision.GetComponentInParent<PlayerHealth>();
            health.TakeDamage(dmg);
            Destroy(this.gameObject);
        }

    }
}
