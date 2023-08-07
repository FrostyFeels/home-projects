using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : EnemyAttack
{

    public GameObject bulletPrefab;
    public float timeTillAttack;

    public Material chargeMaterial;
    public SpriteRenderer chargeMeter;

    public bool charging;


    public Color startColor;
    public Color currentColor;
    public float colorChange;
    private void OnEnable()
    {
        StartCoroutine("PrepareShot");

        chargeMeter.material = chargeMaterial;
        startColor = chargeMeter.material.GetColor("_OutlineColor");

        colorChange = (1.5f / timeTillAttack) / 255;

        currentColor = startColor;
    }

    public void Update()
    {
        if (!inAttack)
            RotateWeapon();

        if(charging)
            setCharge();
    }

    public IEnumerator PrepareShot()
    {
        charging = true;
        chargeMeter.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeTillAttack);
        charging = false;
        Shoot();

        move.SwapStates("Moving");
        chargeMeter.gameObject.SetActive(false);
        chargeMeter.material.SetColor("_OutlineColor", startColor);
    }

    public Vector2 CalculateDirection()
    {
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        return direction;
    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, weaponHolder.transform.rotation);
        enemyBullet _bullet = bullet.GetComponent<enemyBullet>();
        _bullet.dir = CalculateDirection();
    }
    public override void CancelAttack()
    {
        StopCoroutine("PrepareShot");
        chargeMeter.material.SetColor("_OutlineColor", startColor);
        this.enabled = false;
    }

    public void setCharge()
    {  
        Color color = currentColor;

        color.g -= colorChange * Time.fixedDeltaTime;
        currentColor = color;
        chargeMeter.material.SetColor("_OutlineColor", color);
    }
}
