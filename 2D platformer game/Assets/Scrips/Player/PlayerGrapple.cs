using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrapple : MonoBehaviour
{
    [SerializeField] private GameObject grappleShot;
    [SerializeField] private Vector2 dir;
    [SerializeField] private Vector2 movementDir;
    [SerializeField] private Vector2 hitLoction;
    [SerializeField] private float angle;
    [SerializeField] private float grappleSpeed;
    [SerializeField] private float shotSpeed;
    [SerializeField] private float range;


    GameObject cursor;
    GameObject target;
    Rigidbody2D rb;
    PlayerMovement movement;
    [SerializeField] private LineRenderer line;
    [SerializeField] private bool grappeling = false;

    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("Cursor"); 
        rb = gameObject.GetComponent<Rigidbody2D>();
        movement = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = (cursor.transform.position - transform.position);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }

        if(target != null)
        {
            grappeling = true;
            rb.gravityScale = 0f;
            movement.enabled = false;

            line.SetPosition(0, transform.position);
            line.SetPosition(1, hitLoction);        
            
            //When the player jumps or is close to the point of finish he will let go keeping its momentum
            if((Vector2.Distance(transform.position,hitLoction) < range && Vector2.Distance(transform.position, hitLoction) > -range) ||  Input.GetKeyDown(KeyCode.Space))
            {
                target = null;
                movement.enabled = true;
                line.enabled = false;
                movement.wantedSpeed = movementDir.x * grappleSpeed;
                if(movement.wantedSpeed < 0) 
                {
                    movement.wantedSpeed = -movementDir.x * grappleSpeed;
                }               
                if(movementDir.x > 0)
                {
                    movement.direction = 1;

                } else if(movementDir.x < 0)
                {
                    movement.direction = -1;
                }
                movement.Grapplespeeding = true;

            }
        }
    }


    public void Hit(GameObject target, Vector3 position)
    {


        this.target = target;
        movementDir = (position - transform.position).normalized;
        hitLoction = position;
        line.enabled = true;
    }

    public void FixedUpdate()
    {
        if(target != null)
        {
            rb.velocity = movementDir * grappleSpeed;
        }
        
    }

    public void Shoot()
    {
        GameObject grapple = Instantiate(grappleShot, transform.position, Quaternion.identity);
        grapple.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        grapple.GetComponent<Grapple>().speed = shotSpeed;
        grapple.GetComponent<Grapple>().grapple = gameObject.GetComponent<PlayerGrapple>();
     
    }
}
