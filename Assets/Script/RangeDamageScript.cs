using System;
using UnityEngine;

public class RangeDamageScript : DamageScriptBase
{
    public override void Attack()
    {
        if (Character.weaponSlot.ItemInSlot is Bow bow)
        {
            BowAttack(bow);
        }
        else if (Character.weaponSlot.ItemInSlot is Staff staff)
        {
            StaffAttack(staff);
        }
    }

    private void StaffAttack(Staff staff)
    {
        Damage damage = new Damage(Character.physicalAttack.Value, Character.magicPower.Value * 0.2f, Character);
        var tr = Character.CurrentLookVector;
        Projectile.CreateProjectile(staff.ProjectileModel, damage, 60f, GetComponentInParent<WeaponSelector>().leftHand.transform.position, tr, Character);

    }

    private void BowAttack(Bow bow)
    {
        Damage damage = new Damage(Character.physicalAttack.Value, 0, Character);
        var tr = Character.CurrentLookVector;
        Projectile.CreateProjectile(bow.ProjectileModel, damage, 60f, transform.position, tr, Character);
    }
}
