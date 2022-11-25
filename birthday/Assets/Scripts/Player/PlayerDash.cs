using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private float dashspeed;

    public bool dashing = false;
    private Vector3 forceApplied;


    [SerializeField] private float dashTime;
    [SerializeField] private float cooldown, timer;
    private bool onCoolDown = false;
    public void Update()
    {
        if (onCoolDown)
            onCoolDown = !Timer();

        if (health.hit)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && !onCoolDown) { CalculateForceAngle(); }



       

    }
    public void CalculateForceAngle()
    {
        rb.velocity = Vector2.zero;

        float angle = Cursor.CalculateToCursor(transform.position);

        float xAngle = Mathf.Cos(angle * Mathf.PI / 180) * dashspeed;
        float zAngle = Mathf.Sin(angle * Mathf.PI / 180) * dashspeed;

        forceApplied = new Vector3(xAngle, zAngle, 0);
        timer = dashTime;
        dashing = true;

        
    }

    public bool Timer()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            return false;
        }
        timer = cooldown;
        dashing = false;
        onCoolDown = true;
        
        return true;
    }

    public void FixedUpdate()
    {
        if(dashing && !Timer())
        {
            rb.velocity = forceApplied * Time.fixedDeltaTime;
        }
    }

}
