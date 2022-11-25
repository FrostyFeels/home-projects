using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponStats : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    public int dmg;
    public float knockback;
    public float armorPen;
    public int range;
}
