
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
        AttackSpeed = new AttributeSet("Attack rate",AttributeType.AttackSpeed, 0f, 0.8f, "{0} atk/s");
        AttackSpeed.order = 1;
        PhysicalAttack = new AttributeSet("Attack damage", AttributeType.PhysicalAttack,60f, 0.1f);
        AttackRange = new AttributeSet("Attack range",AttributeType.AttackWeaponRange, 30f, 0f);
        MovementSpeed = new AttributeSet("Movement Speed", AttributeType.MovementSpeed, 400f, 0f);
        MovementSpeed.order = -1;
    }
}