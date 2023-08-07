using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("script refrences")]
    public Rigidbody2D rb;
    [SerializeField] private EnemyOnStunned stunned;
    public EnemyMovement movement;
    [SerializeField] private EnemyAttack attack;


    //[SerializeField] bool melee;
    //[SerializeField] bool ranged;
    public bool inCombat = false;

    public float speed;
    public GameObject _Target;


    [Header("Enemies")]
    [SerializeField] private GameObject enemyManager;
    [SerializeField] private List<GameObject> _Enemies = new List<GameObject>();
    [SerializeField] private List<GameObject> _EnemyHolders = new List<GameObject>();
    [SerializeField] private List<GameObject> EnemiesCloseBy = new List<GameObject>();

    [Header("Range")]
    public float attackRadius;
    public float visionRadius;


    [Header("flock")]
    public Vector2 _dir;
    [SerializeField] private float radius = 4f;
    [SerializeField] private float steerWeight;

    public enum EnemyState
    {
        IDLE,
        MOVING,
        ATTACK,
        STUNNED,   
    }

    public void Start()
    {
        enemyManager = GameObject.Find("EnemyManager");

        rb = gameObject.GetComponent<Rigidbody2D>();
        stunned = gameObject.GetComponent<EnemyOnStunned>();
        stunned.ai = this;

        attack = gameObject.GetComponent<EnemyAttack>();
        attack.move = this;

        movement = gameObject.GetComponent<EnemyMovement>();

        foreach (Transform child in enemyManager.transform)
        {
            _EnemyHolders.Add(child.gameObject);
        }

        foreach (GameObject _holder in _EnemyHolders)
        {
            foreach (Transform child in _holder.transform)
            {
                _Enemies.Add(child.gameObject);
            }
        }



        
    }

    public EnemyState enemyState;

    public void SwapStates(string state)
    {
        attack.enabled = false;
        stunned.enabled = false;

        switch (state)
        {
            case "Idle":
                enemyState = EnemyState.IDLE;
                return;
            case "Moving":
                enemyState = EnemyState.MOVING;
                return;
            case "Attacking":
                enemyState = EnemyState.ATTACK;    
                attack.enabled = true;
                return;
            case "Stunned":
                enemyState = EnemyState.STUNNED;
                stunned.enabled = true;
                return;
        }
    }

    public void CombatCheck()
    {
        float distanceToPlayer = Vector2.Distance(_Target.transform.position, transform.position);

        if (distanceToPlayer < visionRadius)
        {
            inCombat = true;

            if (distanceToPlayer > attackRadius)
                SwapStates("Moving");
        }
        else
        {
            inCombat = false;
            SwapStates("Idle");
        }
    }

    public void OnStunned(float duration)
    {
        attack.CancelAttack();
        
        stunned.currentStunTime = 0;
        stunned.stunDuration = duration;

        SwapStates("Stunned");
    }

    //Gets the normalized direction and then checks for enemies closeby
    public void PlayerToEnemyDirection()
    {
        Vector2 direction = -(transform.position - _Target.transform.position);
        direction.Normalize();

        CheckCloseByEnemy();
        Steer(direction);
    }

    //Checks for the enemies closeby
    public void CheckCloseByEnemy()
    {
        foreach (GameObject aEnemy in _Enemies)
        {
            if (Vector2.Distance(aEnemy.transform.position, transform.position) < radius)
            {
                EnemiesCloseBy.Add(aEnemy);
            }
        }
    }

    //Steers the enemies slightly away from eachother to counter being stuck on eachother
    public void Steer(Vector2 dir)
    {
        Vector2 directionChange = dir;
        foreach (GameObject aEnemy in EnemiesCloseBy)
        {
            Vector2 steer = (transform.position - aEnemy.transform.position).normalized;

            directionChange += steer * steerWeight;
        }

        _dir = directionChange;
        EnemiesCloseBy.Clear();
    }
}
