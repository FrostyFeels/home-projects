 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrapple : MonoBehaviour
{
    [SerializeField] private GameObject grapplePrefab;
    [SerializeField] private PlayerHealth health;

    public GameObject grapple;

    public LineRenderer lr;

    [SerializeField] private float speed;

    public bool grappleFired;

    public float grapplePull;

    public Rigidbody2D rb;

    public float distanceMultiplier;

    public bool grappling;
    [SerializeField] private float grappleTime = .15f;

    public void Update()
    {
        if (grappleFired)
            UpdateGrapple();

        if (health.hit)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            Shootgrapple();
        }
    }

    public void FixedUpdate()
    {
        if (grapple != null)
            grapple.transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
    }

    public void Shootgrapple()
    {
        float angle = Cursor.CalculateToCursor(transform.position);

        grapple = Instantiate(grapplePrefab, transform.position, Quaternion.Euler(0, 0, angle));
        Grapple grappleScript = grapple.GetComponent<Grapple>();
        grappleScript.grapple = this;
        grappleFired = true;
        lr.enabled = true;
    }

    public void UpdateGrapple()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, grapple.transform.position);
    }

    public void PullEnemy(Vector3 force, Rigidbody2D enemy)
    {
        enemy.AddForce(force);
    }

    public void PullPlayer(Vector3 force, Rigidbody2D player)
    {
        grappling = true;
        rb.AddForce(force);
        StartCoroutine(StopGrapple());
    }

    IEnumerator StopGrapple()
    {      
        yield return new WaitForSeconds(grappleTime);
        grappling = false;
    }



}
