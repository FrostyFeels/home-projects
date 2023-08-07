using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBullet : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private int speed;

    Rigidbody2D rb;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDirection();
    }

    public void CalculateDirection()
    {
        direction = ((Vector2)target.transform.position - (Vector2)transform.position).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            Debug.Log("OwO");
            Destroy(this.gameObject);
        }

    }
}
