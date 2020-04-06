
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Inventory System System/Items/Weapon")]
public class Weapon : Equipment
{
    public AttackType AttackType;

    public override void Initialize()
    {
        id = 123;
        name = "Ostrze cieni";
        requiredLevel = 0;
        AttackType = AttackType.Range;
        AttackSpeed = new AttributeSet(AttributeType.AttackSpeed, 0.9f, 0, "atk/s");
        AttackSpeed.Name = "Attack rate";
        AttackSpeed.order = 1;
        PhysicalAttack = new AttributeSet(AttributeType.PhysicalAttack,60f, 0.1f);
        AttackRange = new AttributeSet(AttributeType.AttackWeaponRange, 30f, 0f);
        MovementSpeed = new AttributeSet(AttributeType.MovementSpeed, 400f, 0f);
        MovementSpeed.order = -1;
    }
}