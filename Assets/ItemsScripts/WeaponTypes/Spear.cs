using UnityEngine;

[CreateAssetMenu(fileName = "New spear", menuName = "Inventory System Items/Weapon/Axe")]
public class Spear : Weapon
{
    public override AttackType AttackType { get; } = AttackType.Melee;

    public override void Initialize()
    {
    }

}
