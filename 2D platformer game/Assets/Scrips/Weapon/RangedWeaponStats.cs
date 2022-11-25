using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Weapon/Ranged")]
public class RangedWeaponStats : WeaponStats
{
    public float fireRate;
    public float reloadSpeed;
    public float minSpread;
    public float maxSpread;
    public int piercing;
    public float BulletSpeed;
    public int ammo;
    public int headShotDamage;
    public int NumberOfShots;
    public bool autoShooting;
    public int lifeSteal;
    public float shieldDmg;
}
