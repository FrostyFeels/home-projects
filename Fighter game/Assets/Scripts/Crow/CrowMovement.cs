using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowMovement : MonoBehaviour
{

    [SerializeField] private float maxX, minX;
    [SerializeField] private float maxY, minY;

    [SerializeField] private float currentLoopTime;
    [SerializeField] private float loopTime;

    //public float birdcd, curbirdcd;

    private Vector2 currentPosition;
    private Vector2 wantedPosition;


    public bool inUse; //when on enemy
    public bool move; //when being around player
    public bool send; //when flying towards cursor

    [SerializeField] float speed;
    [SerializeField] float flightTime;

    Rigidbody2D rb;
    Transform holder;

    private Vector2 dir;

    public void Start()
    {
        minX = maxX * -1;
        minY = maxY * -1;

        
       holder = transform.parent;
       rb = gameObject.GetComponent<Rigidbody2D>();
       StartCoroutine("MovePosition");


 
    }

    public void Update()
    {
        if(loopTime > currentLoopTime && move)
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, wantedPosition, (currentLoopTime / loopTime));
            currentLoopTime += Time.deltaTime;
        }


        if (Input.GetKeyDown(KeyCode.Q) && inUse)
        {
            transform.parent = holder.transform;
            inUse = false;

        }

        if (Input.GetKeyDown(KeyCode.Q) && !inUse)
        {
            StartCoroutine("ResetBird");
            StopCoroutine("MovePosition");
            inUse = true;
            send = true;
            move = false;
            SendBird();
        }

   


    }

    //Moves to a random position around the player
    IEnumerator MovePosition()
    {
        float xpos = Random.Range(minX, maxX);
        float ypos = Random.Range(minY, maxY);

        wantedPosition = new Vector2(xpos, ypos);
        currentLoopTime = 0;
        yield return new WaitForSeconds(4f);

        StartCoroutine("MovePosition");
    }

    //Shoot the bird out to mouse
    public void SendBird()
    {
        transform.SetParent(null);

        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousepos - (Vector2)transform.position).normalized;

        Debug.Log(direction);
        dir = direction;

    }

    public void FixedUpdate()
    {
        if(send)
            rb.velocity = dir * speed * Time.fixedDeltaTime;
    }

    //if nothing is hit after hitting flightime is over reset parent to player
    public IEnumerator ResetBird()
    {
        yield return new WaitForSeconds(flightTime);

        if(transform.parent == null)
        {
            transform.parent = holder;
            transform.localPosition = wantedPosition;
            inUse = false;
            move = true;
            send = false;
            StartCoroutine("MovePosition");
        }

        rb.velocity = Vector2.zero;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!move && collision.gameObject.CompareTag("Enemy"))
        {
            transform.parent = collision.gameObject.transform;
            rb.velocity = Vector2.zero;
            move = true;
            StartCoroutine("MovePosition");
        }
    }
}
