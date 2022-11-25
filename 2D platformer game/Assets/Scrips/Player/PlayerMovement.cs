using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float currentSpeed, wantedSpeed;
    [SerializeField] private float sprintMultiplyer;
    public float maxSpeed, maxRunningSpeed, speed;
    [SerializeField] private float runningAcceleration, acceleration, deacceleration, airDeAcceleration;
    [SerializeField] private float timeToMax, timeToMaxRunningSpeed, timeToStill, airToStill;
    public int direction, oldDirection;
    public bool Grapplespeeding;


    [SerializeField] private float rotation, runningRotation;

    [SerializeField] private Transform body;

    Rigidbody2D rb;
    
    PlayerJumping jump;
    
    // Start is called before the first frame update
    void Start()
    {
        maxRunningSpeed = maxSpeed * sprintMultiplyer;
        runningAcceleration = (maxRunningSpeed - maxSpeed) / timeToMaxRunningSpeed;
        acceleration = (maxSpeed - speed) / timeToMax;
        deacceleration = maxRunningSpeed / timeToStill;
        airDeAcceleration = maxSpeed / airToStill;

        rb = gameObject.GetComponent<Rigidbody2D>();
        jump = gameObject.GetComponent<PlayerJumping>();
    }

    // Update is called once per frame
    void Update()
    {
        //sets the oldDirection to the new direction
        oldDirection = direction;

        
        //Gives speed to the player when he starts walking.
        if (Input.GetKeyDown(KeyCode.D)) 
        { 
            currentSpeed = speed;
            Debug.Log("Runs");
        } else if(Input.GetKeyDown(KeyCode.A)) 
        {  
            currentSpeed = speed;
        }
        //Gives acceleration when the player is holding the key
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !Input.GetKey(KeyCode.LeftShift))
        {
            body.transform.rotation = Quaternion.Euler(0f, 0f, rotation * direction);
            currentSpeed += acceleration * Time.deltaTime;                    
        }
        //Gives the player deacceleration when the player is not holding any keys
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            if (jump.isGrounded())
            {
                currentSpeed -= deacceleration * Time.deltaTime;
            }
            if (!jump.isGrounded())
            {
                currentSpeed -= airDeAcceleration * Time.deltaTime;
            }
            if (currentSpeed <= 0)
            {
                currentSpeed = 0;
            }

            body.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        //Gives the player a direction that they walk towards 
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) { direction = 1; } else if(Input.GetKey(KeyCode.A) &&!Input.GetKey(KeyCode.D)) { direction = -1; }
        //Makes the player stand still when holding both of the keys at the same time
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
        {

                ;
            Debug.Log("Runs");
        }

        //Clamps the speed depending if the player is running or walking
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            Debug.Log("whyt");
            currentSpeed += runningAcceleration * Time.deltaTime;
            body.transform.rotation = Quaternion.Euler(0f, 0f, runningRotation * direction);
            if(!Grapplespeeding)
            {
                currentSpeed = Mathf.Clamp(currentSpeed, -maxRunningSpeed, maxRunningSpeed);
            }
            
        }
        else if (!Grapplespeeding)
        {
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
        }

        //If grappeling the player has a diffrent max speed
        if(Grapplespeeding)
        {
            currentSpeed = Mathf.Clamp(currentSpeed, -wantedSpeed, wantedSpeed);
            currentSpeed = wantedSpeed;
            
            if(jump.isGrounded())
            {
                wantedSpeed -= deacceleration * Time.deltaTime;
            }
            if(!jump.isGrounded())
            {
                wantedSpeed -= airDeAcceleration * Time.deltaTime;
            }
           

            if(wantedSpeed <= maxRunningSpeed && Input.GetKey(KeyCode.LeftShift))
            {
                Grapplespeeding = false;
            }

            if (wantedSpeed <= maxSpeed && !Input.GetKey(KeyCode.LeftShift))
            {
                Grapplespeeding = false;
            }



        }

        //Checks if player has turned around so the sprintspeed will reset
        if (oldDirection != direction)
        {
            currentSpeed = speed;
            wantedSpeed = speed;
        }
    }

    private void FixedUpdate()
    {
        //makes the player move
        rb.velocity = new Vector2(currentSpeed * direction, rb.velocity.y);            
    }
}
