using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageScriptBase : MonoBehaviour
{
    public Character Character { get; set; }
    protected float _raysHeight;
    public float range = 2.3f;
    public bool CanDealDamage { get; protected set; }
    protected int raysNumber = 4;

    public Transform weaponHandlerTransform;

    protected void Start()
    {
        Character = GetComponentInParent<Character>();
        _raysHeight = Character.GetComponent<CharacterController>().height;
    }

    public virtual void Attack(Collider other)
    {
    }

    public virtual void Attack()
    {
    }

    protected Transform GetWeaponHandler(Transform t)
    {
        if (t.parent == null)
        {
            return null;
        }

        if (!t.parent.tag.Equals("WeaponHandler"))
        {
            return GetWeaponHandler(t.parent);
        }

        return t.parent;
    }
}