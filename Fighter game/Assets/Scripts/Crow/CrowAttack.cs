using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowAttack : MonoBehaviour
{
    [SerializeField] private float range;

    [SerializeField] private LayerMask enemy;

    [SerializeField] private List<EnemyMovement> enemiesInRange; 
    [SerializeField] private GameObject target;
    private CrowMovement movement;

    [SerializeField] private GameObject bulletPrefab;

    private bool attacking;
    private bool rangeChecking;

    [SerializeField] private float attackSpeed;

    [SerializeField] private GameObject VfxHolder;


    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponent<CrowMovement>();
        StartCoroutine("rangeCheck");
        rangeChecking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(movement.inUse)
        {
            StopCoroutine("rangeCheck");
            rangeChecking = false;
            if (transform.parent != null)
                target = transform.parent.gameObject;
        }

        if (!rangeChecking && !movement.inUse)
        {
            StartCoroutine("rangeCheck");
            rangeChecking = true;
        }


        if(target != null && !attacking)
        {
            attacking = true;
            StartCoroutine("Attack");
        }

    }

    public IEnumerator rangeCheck()
    {
        yield return new WaitForSeconds(1f);

        if (enemiesInRange.Count > 0)
        {
            CheckOutrange();
            DistanceCheck();
        }
           

        if (target == null)
            CheckInRange();

     
            

        StartCoroutine("rangeCheck");
    }

    //Checks enemies in range
    public void CheckInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, enemy);

        foreach (Collider2D _hit in hits)
        {
            if(_hit.gameObject.CompareTag("Enemy"))
            {
                var enemyRange = _hit.GetComponent<EnemyMovement>();
                if (enemyRange.inCombat && !enemiesInRange.Contains(enemyRange))
                {
                    enemiesInRange.Add(enemyRange);
                }
            }
        }

        DistanceCheck();
    }

    //Gets closest enemy
    public void DistanceCheck()
    {
        float smallestDistance = range + 1; //to make sure it starts with highest distance

        foreach (var _enemy in enemiesInRange)
        {
            float distance = Vector2.Distance(transform.position, _enemy.transform.position);
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                target = _enemy.gameObject;
            }
        }
    }

    //Checks if enemies still in range
    public void CheckOutrange()
    {
        List<EnemyMovement> outOfRange = new List<EnemyMovement>(); 
        foreach (var _enemy in enemiesInRange)
        {
            float distance = Vector2.Distance(_enemy.transform.position, transform.position);
            if(!_enemy.inCombat || distance > range)
            {
                outOfRange.Add(_enemy);
            }
        }

        foreach (var _enemy in outOfRange)
        {
            if (target == _enemy.gameObject)
            {
                StopCoroutine("Attack");
                attacking = false;
                target = null;
            }
                

            enemiesInRange.Remove(_enemy);
        }


    }



    public IEnumerator Attack()
    {
        if(movement.inUse)
        {
            StartCoroutine("Slash");
        }
        else
        {
            Shoot();
        }

        yield return new WaitForSeconds(1f / attackSpeed);
        StartCoroutine("Attack");
    }

    public IEnumerator Slash()
    {

        Vector3 direction = (target.transform.position - transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        VfxHolder.transform.rotation = Quaternion.Euler(0, 0, angle);
        VfxHolder.SetActive(true);

        yield return new WaitForSeconds(.5f);

        VfxHolder.SetActive(false);
    }

    public void Shoot()
    {
        float angle = Cursor.CalculateToCursor(transform.position);

        GameObject _bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
        CrowBullet crowshot = _bullet.GetComponent<CrowBullet>();
        crowshot.target = target;
       
    }
}
