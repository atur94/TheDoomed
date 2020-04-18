
using System;
using UnityEngine;

public abstract class Weapon : Equipment
{
    public virtual AttackType AttackType => _attackType;

    [SerializeField]
    private GameObject projectilePrefab;

    public Transform weaponOffset;
    private readonly AttackType _attackType;

    public GameObject ProjectileModel
    {
        get
        {
            GameObject projectile;
            if (projectilePrefab == null)
            {
                projectile = GameObject.CreatePrimitive(PrimitiveType.Cube);
            }
            else
            {
                projectile = Instantiate(projectilePrefab);
            }
            projectile.transform.localScale = projectile.transform.localScale * 0.3f;

            return projectile;
        }
    }
}