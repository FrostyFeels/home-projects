using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] ranged;
    [SerializeField] private GameObject[] melee;
    [SerializeField] private int currentRanged;
    [SerializeField] private int currentmelee;

    private Transform player;
    [SerializeField] private bool usingRanged = false, usingMelee = false;
    [SerializeField] private bool scrollUp, scrollDown;

    public void Start()
    {
        currentRanged = 0;
        currentmelee = 0;
        SwitchRanged();

        player = GameObject.Find("Player").GetComponent<Transform>();
    }


    public void Update()
    {
        transform.position = player.position;
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(usingRanged)
            {
                scrollUp = true;
                SwitchRanged();
               
            }

            if(usingMelee)
            {
                scrollUp = true;
                SwitchMelee();
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (usingRanged)
            {
                scrollDown = true;
                SwitchRanged();

            }

            if (usingMelee)
            {
                scrollDown = true;
                SwitchMelee();
            }
        }



    }

    public void SwitchRanged()
    {
        //When the player scrolls up check if you at the max number of weapons, set currentweapon to 0 if max reached
        if (scrollUp)
        {
            if (currentRanged >= ranged.Length - 1)
            {
                currentRanged = 0;
                ranged[currentRanged].SetActive(true);
            }
            else 
            { 
                currentRanged++;
                ranged[currentRanged].SetActive(true);
            }
     
        }
        //When the player scrolls down check if you at the bottom, set currentweapon max if min reached
        if(scrollDown)
        {
            if (currentRanged == 0)
            {
                currentRanged = ranged.Length - 1;
                ranged[currentRanged].SetActive(true);
            }
            else 
            { 
                currentRanged--;
                ranged[currentRanged].SetActive(true);
            }
           
        }

        //set the other weapons off
        for (int i = 0; i < ranged.Length; i++)
        {
            if(i != currentRanged)
            {
                ranged[i].SetActive(false);
            }
        }


        //if player doesnt have the weapon unlocked dont let them use it
        if (PlayerArsenal.CheckWeapon(ranged[currentRanged].name))
        {
            Debug.Log("Works: " + ranged[currentRanged].name);
            Debug.Log("Works: " + currentRanged);
        }
        else
        {
            SwitchRanged();
            Debug.Log("Doesnt work " + ranged[currentRanged].name);
            Debug.Log("Doesnt work " + currentRanged);
        }

        scrollUp = false;
        scrollDown = false;

    }



    public void SwitchMelee()
    {
        if (currentmelee >= melee.Length)
        {
            ranged[currentmelee].SetActive(true);
        }
    }
}
