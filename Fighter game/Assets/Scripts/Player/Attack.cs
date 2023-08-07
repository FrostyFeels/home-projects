using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;
using UnityEditor.Audio;

public class Attack : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Sound[] attacksSounds;

    [SerializeField] private ScreenShake shake;

    [SerializeField] private Transform weaponHolder;
    [SerializeField] private GameObject VFXHolder;

    [SerializeField] private Rigidbody2D player;

    public List<VisualEffect> effect;
    public GameObject bloodprefab;
    [SerializeField] private Transform bloodHolder;
    
    
    public int combo;

    [SerializeField] private float pushForce;
    [SerializeField] private float moveForce;


    [SerializeField] private float cooldown;
    [SerializeField] private float comboResetTime;
    private float timeSinceLastSlash;

    [SerializeField] private List<SlashInfo> slashes = new List<SlashInfo>();

    private bool attacking = false;
    private bool hasAttacked = false;
    public bool stuckPlayer = false;

    [SerializeField] private float stuckTime = .05f;

    private Vector3 lastDir;

    [SerializeField] private float distanceMultiplier;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            player.velocity = Vector2.zero;
            stuckPlayer = true;
            attacking = true;
            StartCoroutine(SlashAttack());
        }

        if (hasAttacked)
            Timer();

    }

    IEnumerator SlashAttack()
    {
      
        int id = UnityEngine.Random.Range(0, attacksSounds[combo].clip.Length);
        audioSource.PlayOneShot(attacksSounds[combo].clip[id]);
        

        
        timeSinceLastSlash = 0;
        PushPlayer();
        shake.GetNeededLocation();
        VFXHolder.transform.eulerAngles = weaponHolder.eulerAngles;
        
        foreach (GameObject _slash in slashes[combo].slash)
        {
            _slash.SetActive(true);
        }

        foreach (GameObject _slash in slashes[combo].hitbox)
        {
            _slash.SetActive(true);
        }

        yield return new WaitForSeconds(stuckTime);
        DisableHitBox();
        stuckPlayer = false;
                


        yield return new WaitForSeconds(effect[combo].GetFloat("LifeTime") - stuckTime);
   
        DisableSlash();
        
        
        attacking = false;
        hasAttacked = true;

        ChangeCombo(true);
        CheckEndCombo();
    }
    void DisableSlash()
    {
        
        foreach (GameObject _slash in slashes[combo].slash)
        {
            _slash.SetActive(false);
            Debug.Log(combo);
        }


    }
    void DisableHitBox()
    {
        foreach (GameObject _slash in slashes[combo].hitbox)
        {
            _slash.SetActive(false);
        }
    }
    public void Timer()
    {
        if (timeSinceLastSlash > comboResetTime)
        {
            timeSinceLastSlash = 0;
            hasAttacked = false;
            ChangeCombo(false);
        }
        timeSinceLastSlash += Time.deltaTime;
    }
    public void ChangeCombo(bool keep)
    {
        if (keep)
            combo += 1;
        else
            combo = 0;
    }
    public bool CheckEndCombo()
    {
        if (combo == slashes.Count)
        {
            attacking = true;
            hasAttacked = false;
            combo = 0;
            StartCoroutine(ComboEnd());
            return true;
        }
        return false;
    }
    public IEnumerator ComboEnd()
    {
        yield return new WaitForSeconds(cooldown);
        attacking = false;
    }

    [Serializable]
    public class SlashInfo
    {
        public GameObject[] slash;
        public GameObject[] hitbox;
    }

    public void PushEnemy(Rigidbody2D Enemy)
    {

        float distance = Vector2.Distance(Enemy.transform.position, transform.position) * distanceMultiplier;

        Vector3 pushforce = CalculateForce(pushForce - distance);

        Enemy.velocity = pushforce;
        lastDir = pushforce;
    }

    public void PushPlayer()
    {
        player.AddForce(CalculateForce(moveForce), ForceMode2D.Impulse);
    }


    public Vector3 CalculateForce(float force)
    {
        float angle = Cursor.CalculateToCursor(transform.position);

        float xAngle = Mathf.Cos(angle * Mathf.PI / 180) * force;
        float zAngle = Mathf.Sin(angle * Mathf.PI / 180) * force;

        Vector3 forceApplied = new Vector3(xAngle, zAngle, 0);
        
        return forceApplied;
    }


}



