using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private int xDir, yDir, oldXDir, oldYDir;

    [SerializeField] private float  ySpeed, xSpeed;

    [SerializeField] private int startSpeed, maxSpeed;
    [SerializeField] private float acceleration, deacceleration;
    [SerializeField] private float timeToMax, timeToStop;

    [SerializeField] private PlayerDash playerDash;
    [SerializeField] private Attack playerAttack;
    [SerializeField] private PlayerGrapple grapple;
    [SerializeField] private PlayerHealth health;


    public void Start()
    {
        acceleration =   (maxSpeed - startSpeed) / timeToMax;
        deacceleration = (maxSpeed - startSpeed) / timeToStop;
    }
    public void Update()
    {
     

        GetDirection();
        CheckDirectionChange();

        if (playerAttack.stuckPlayer || playerDash.dashing)
        {
            ySpeed = 0;
            xSpeed = 0;
            xDir = 0;
            yDir = 0;
            oldXDir = 0;
            oldYDir = 0;
        }

        if (playerDash.dashing || playerAttack.stuckPlayer)
            return;

        ApplyAcceleration();
      
        ConstrainSpeed();
    }

    public void GetDirection()
    {
        if (Input.GetKey(KeyCode.W)) { yDir = 1; }
        else if (Input.GetKey(KeyCode.S)) { yDir = -1; }
        else if (ySpeed == 0) { yDir = 0; }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) { yDir = 0; }


        if (Input.GetKey(KeyCode.D)) { xDir = 1; }
        else if (Input.GetKey(KeyCode.A)) { xDir = -1; }
        else if (xSpeed == 0){ xDir = 0; }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)) { xDir = 0; }
    }
    public void ApplyAcceleration()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) { ySpeed += acceleration; }
        else { ySpeed -= deacceleration; }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) { xSpeed += acceleration; }
        else { xSpeed -= deacceleration; }
    }
    public void CheckDirectionChange()
    {
        if (xDir != oldXDir && xDir != 0) { xSpeed = startSpeed; }
        if (yDir != oldYDir && yDir != 0) { ySpeed = startSpeed; }

        oldYDir = yDir;
        oldXDir = xDir;
    }
    public void ConstrainSpeed()
    {
        xSpeed = Mathf.Clamp(xSpeed, 0, maxSpeed);
        ySpeed = Mathf.Clamp(ySpeed, 0, maxSpeed);
    }

    public void FixedUpdate()
    {
        if (playerDash.dashing || playerAttack.stuckPlayer || grapple.grappling || health.hit)
            return;
        rb.velocity = new Vector2(xDir * xSpeed, yDir * ySpeed) * Time.fixedDeltaTime;
    }

}
