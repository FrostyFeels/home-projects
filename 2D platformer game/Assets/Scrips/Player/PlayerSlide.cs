using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D bc;
    PlayerMovement movement;
    [SerializeField] private Transform body;

    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideTime;
    [SerializeField] private int rotation;
    [SerializeField] private bool isSliding;
    [SerializeField] private bool stopSliding;

    [SerializeField] LayerMask groundMask;


    void Start()
    {
        bc = gameObject.GetComponentInChildren<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        movement = gameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //Starts sliding if not already sliding and on the ground and when moving
        //Stops sliding if jump is pressed
        if (Input.GetKeyDown(KeyCode.S) && IsGrounded() && !isSliding && movement.direction != 0)
        {
            body.transform.rotation = Quaternion.Euler(0f, 0f, rotation * movement.direction);
            StartCoroutine(StopSliding());
            movement.enabled = false;
            isSliding = true;

        }
        if(isSliding && Input.GetKeyDown(KeyCode.Space)) 
        {
            stopSliding = true;
        }

        if(stopSliding)
        {
            isSliding = false;
            movement.enabled = true;
            movement.Grapplespeeding = true;
            movement.wantedSpeed = slideSpeed;
            body.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            stopSliding = false;
            Debug.Log("Runs");
        }


    }

    private void FixedUpdate()
    {
        if(isSliding)
        {
            rb.velocity = new Vector2(slideSpeed * movement.direction, rb.velocity.y);
        }
    }

    IEnumerator StopSliding()
    {       
        yield return new WaitForSeconds(slideTime);
        if(isSliding)
        {
            stopSliding = true;
        }
                   
    }


    public bool IsGrounded()
    {
        RaycastHit2D raycasthit2d = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, groundMask);
        return raycasthit2d.collider != null;
    }
}
