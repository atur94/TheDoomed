
using System;
using UnityEngine;

public abstract class Weapon : Equipment
{
    public virtual AttackType AttackType { get; }
    [SerializeField]
    private GameObject projectilePrefab;

    public GameObject ProjectileModel
    {
        get
        {
            GameObject projectile;
            if (projectilePrefab == null)
            {
                projectile = Resources.Load<GameObject>("DefaultProjectile");
                projectile.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            }
            else
            {
                projectile = Instantiate(projectilePrefab);
            }

            return projectile;
        }
    }


    public enum WeaponType
    {
        NoWeapon, Sword, Bow, Staff, Spear, Axe, GreatSword
    }
    //  0,          1,    2 ,   3   ,  3  ,  4 ,      5 
}