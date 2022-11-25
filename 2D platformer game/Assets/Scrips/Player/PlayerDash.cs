using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    //When player presses cntr and a or d dash to certain direction
    //Add a animation when you teleport
    //Teleport player to the new location(no velocity bonus)
    Transform player;
    Rigidbody2D rb;
    PlayerMovement movement;
    [SerializeField] private float distance;

    private bool isDashing = false;
    private bool cooldown = false;

    private void Start()
    {
        player = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        movement = gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && movement.direction != 0 && !cooldown && movement.currentSpeed != 0)
        {          
            cooldown = true;
            isDashing = true;
            StartCoroutine(StopDashing());
        }
    }

    public void FixedUpdate()
    {
        if(isDashing)
        {
            player.position += new Vector3(distance * movement.direction, 0, 0);
            isDashing = false;
        }
    }

    IEnumerator StopDashing()
    {

        yield return new WaitForSeconds(2f);
        cooldown = false;
    }
}
