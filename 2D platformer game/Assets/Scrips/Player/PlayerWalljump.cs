using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalljump : MonoBehaviour
{
    [SerializeField] private float wallJumpTime = 0.2f;
    [SerializeField] private float wallSlideSpeed = .5f;
    [SerializeField] private float wallDistance = 1f;
    [SerializeField] private float airTime;
    float jumpTime;

    private bool isWallSliding = false;
    private bool wallJump;
    private bool rightWall, leftWall;

    RaycastHit2D wallCheckHit;
    PlayerMovement movement;
    PlayerJumping jump;
    BoxCollider2D bc;
    Rigidbody2D rb;

    [SerializeField] LayerMask wallmask;
    [SerializeField] LayerMask groundMask;


    void Start()
    {
        bc = gameObject.GetComponentInChildren<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        movement = gameObject.GetComponent<PlayerMovement>();
        jump = gameObject.GetComponent<PlayerJumping>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the player is wallsliding already and presses the space key he will perform a jump
        if(isWallSliding && Input.GetKeyDown(KeyCode.Space))
        {
            wallJump = true;
        }
    }

    private void FixedUpdate()
    {
        //Raycast to check if your against a wall
        if (movement.direction == 1)
        {
            wallCheckHit = Physics2D.Raycast(transform.position, Vector2.right, wallDistance, wallmask);
            if(wallCheckHit)
            {
                rightWall = true;
            }
            
            
        } else
        {
            wallCheckHit = Physics2D.Raycast(transform.position, Vector2.left, wallDistance, wallmask);
            if (wallCheckHit)
            {
                leftWall = true;
            }

        }

        //Checks if you in the air and still moving against the wall.
        if(wallCheckHit && !isGrounded() && movement.currentSpeed != 0)
        {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
        } else if(jumpTime < Time.time)
        {
            isWallSliding = false;
            rightWall = false;
            leftWall = false;
        }

        //Slowly slide off the wall
        if(isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallSlideSpeed, float.MaxValue));
        }

        //Does the walljump right here
        if(wallJump)
        {
            //If facing a wall to the right, send player to the right
            if(rightWall)
            {               
                StartCoroutine(StopMovement());
                movement.direction = -1;
            
                rb.velocity = new Vector2(-25, jump.jumpForce);
                rightWall = false;
                wallJump = false;
            }
            //If facing a wall to the left, send player to the left
            if(leftWall)
            {          
                StartCoroutine(StopMovement());
                movement.direction = 1;
                

                rb.velocity = new Vector2(25, jump.jumpForce);
                wallJump = false;
                leftWall = false;
            }
        }
    }
    
    //Makes the player lose control for a few frames
    IEnumerator StopMovement()
    {      
        movement.enabled = false;
        yield return new WaitForSeconds(airTime);
        movement.currentSpeed = 15;
        movement.enabled = true;
    }

    public bool isGrounded()
    {
        RaycastHit2D raycasthit2d = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, groundMask);
        return raycasthit2d.collider != null;
    }
}
