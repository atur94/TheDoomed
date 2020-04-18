using UnityEngine;

[CreateAssetMenu(fileName = "New spear", menuName = "Inventory System Items/Weapon/Axe")]
public class Spear : Weapon
{
    private AttackType _attackType = AttackType.Melee;

    public override AttackType AttackType => _attackType;

    public override void Initialize()
    {
    }

}
