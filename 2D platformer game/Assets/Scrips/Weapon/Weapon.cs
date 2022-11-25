using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public RangedWeaponStats weaponStats;

    private int damage;
    private int headshotDamage;
    private int piercing;
    private int range;
    private int ammo;
    private int maxAmmo;
    private int numberOfShots;
    private int lifeSteal;

    private float AS;
    private float reloadSpeed;
    private float minSpread;
    private float maxSpread;
    private float knockback;
    private float bulletSpeed;
    private float shieldDamage;
    private float armorPen;

    private bool autoShooting;

    [SerializeField] private float nextTimeToFire;

    private float angle;

    private bool reloading;
    private Vector2 dir;

    [SerializeField] private GameObject bulletPrefab;
    Rigidbody2D rb;

    Transform cursor;
    Transform weapon;
    PlayerMovement movement;

    


    // Start is called before the first frame update
    void Start()
    {
        damage = weaponStats.dmg;
        headshotDamage = weaponStats.headShotDamage;
        piercing = weaponStats.piercing;
        armorPen = weaponStats.armorPen;
        range = weaponStats.range;
        ammo = weaponStats.ammo;
        maxAmmo = ammo;
        numberOfShots = weaponStats.NumberOfShots;
        lifeSteal = weaponStats.lifeSteal;

        AS = weaponStats.fireRate;
        reloadSpeed = weaponStats.reloadSpeed;
        minSpread = weaponStats.minSpread;
        maxSpread = weaponStats.maxSpread;
        knockback = weaponStats.knockback;
        bulletSpeed = weaponStats.BulletSpeed;
        shieldDamage = weaponStats.shieldDmg;
        armorPen = weaponStats.armorPen;

        autoShooting = weaponStats.autoShooting;

        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        cursor = GameObject.Find("Cursor").GetComponent<Transform>();
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        weapon = gameObject.GetComponent<Transform>();
     
    }

    private void OnEnable()
    {
        reloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        dir = (cursor.transform.position - transform.position).normalized;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if(transform.position.x > cursor.position.x)
        {
            transform.localScale = new Vector2(1,-1);
        }
        if(transform.position.x < cursor.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }

        if (reloading)
             return;


        if (ammo <= 0 || (ammo < maxAmmo && Input.GetKeyDown(KeyCode.R)))
        {
            StartCoroutine(Reloading());
        }
        if (!autoShooting)
        {
            if (Input.GetMouseButtonDown(0) && nextTimeToFire < Time.time)
            {
                nextTimeToFire = Time.time + (1 / AS);
                Shooting();
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && nextTimeToFire < Time.time)
            {
                nextTimeToFire = Time.time + (1 / AS);
                Shooting();
            }
        }

   
    }

    void Shooting()
    {
        for (int i = 0; i < numberOfShots; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, weapon.position, Quaternion.Euler(0f, 0f, angle + 90f));
            bullet.transform.Rotate(0f, 0f, Random.Range(minSpread, maxSpread));
            Bullet bulletStats = bullet.GetComponent<Bullet>();
            bulletStats.speed = bulletSpeed;
        }

        ammo--;
        if(dir.x > 0)
        {
            movement.direction = -1;
            movement.currentSpeed = dir.x * knockback;
        }
        if(dir.x < 0)
        {
            movement.direction = 1;
            movement.currentSpeed += -dir.x * knockback;
        }
        rb.velocity += new Vector2(rb.velocity.x, -dir.y * knockback);
        

        
        
        
    }

    private void FixedUpdate()
    {
        rb.velocity = rb.velocity;
    }
    IEnumerator Reloading()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadSpeed);
        ammo = maxAmmo;
        reloading = false;
       
    }
}
