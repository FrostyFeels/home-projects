using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    public bool hit; //for stopping movement when hit
    private bool unhittable; //for making sure you cant get hit again

    [SerializeField] private float knockback;

    [SerializeField] private float invisTime;
    [SerializeField] private float knockbackTime;


    public void TakeDamage(float dmg)
    {
        if (hit || unhittable)
            return;

        hit = true;
        unhittable = true;

        StartCoroutine("InvisTimer");
        health -= dmg;


        if (health <= 0)
        {
            GameOver();
        }
    }



    public void OnHit(Vector2 pos)
    {
       
        this.GetComponent<Rigidbody2D>().velocity = CalculateForce(pos);
    }

    public Vector3 CalculateForce(Vector2 pos)
    {
  
        Vector2 direction = (pos - (Vector2)transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float xAngle = Mathf.Cos(angle * Mathf.PI / 180) * knockback;
        float zAngle = Mathf.Sin(angle * Mathf.PI / 180) * knockback;

        Vector3 forceApplied = new Vector3(xAngle, zAngle, 0);

        return -forceApplied;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    public IEnumerator InvisTimer()
    {
        yield return new WaitForSeconds(knockbackTime);
        hit = false;
        yield return new WaitForSeconds(invisTime - knockbackTime);
        unhittable = false;
    }

}
