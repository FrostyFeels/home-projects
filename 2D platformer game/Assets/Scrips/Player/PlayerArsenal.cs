using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerArsenal
{
    public static bool pistol = true, semi = true, auto = true, shotgun = true, sniper = true;
    public static bool doubleJump = false, sliding = false, wallJump = false, grappleHook = false, dash = false;


    public static bool CheckWeapon(string weaponName)
    {
        switch (weaponName)
        {
            case "Pistol":
                return pistol;
            case "Semi":
                return semi;
            case "Auto":
                return auto;
            case "Shotgun":
                return shotgun;
            case "Sniper":
                return sniper;         
        }
        return false;
    }
}
