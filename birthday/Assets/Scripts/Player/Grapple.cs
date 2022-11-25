using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public PlayerGrapple grapple;

    Rigidbody2D rb;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            rb = collision.GetComponent<Rigidbody2D>();
        }
        else
            return;

        grapple.grappleFired = false;
        grapple.grapple = null;

        grapple.PullEnemy(CalculateForce(grapple.grapplePull), rb);
        grapple.PullPlayer(-CalculateForce(grapple.grapplePull), grapple.rb);




        grapple.lr.enabled = false;


        Destroy(this.gameObject);
    }

    public float CalculateSpeedOnDistance()
    {

        float distance = Vector2.Distance(rb.transform.position, grapple.rb.transform.position) * grapple.distanceMultiplier;
        return distance;
    }


    public Vector3 CalculateForce(float force)
    {
        float angle = CalculateToGrapple(transform.position);

        float xAngle = Mathf.Cos(angle * Mathf.PI / 180) * force * CalculateSpeedOnDistance();
        float zAngle = Mathf.Sin(angle * Mathf.PI / 180) * force * CalculateSpeedOnDistance();

        Vector3 forceApplied = new Vector3(xAngle, zAngle, 0);
        return forceApplied;
    }
    public float CalculateToGrapple(Vector2 ownPos)
    {
        Vector2 pos = grapple.transform.position;
        Vector2 direction = (pos - (Vector2)ownPos);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        return angle;
    }

}
