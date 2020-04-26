using UnityEngine;

[CreateAssetMenu(fileName = "New axe", menuName = "Inventory System Items/Weapon/Spear")]
public class Axe : Weapon
{
    public override AttackType AttackType { get; } = AttackType.Melee;

    public override void Initialize()
    {
    }
}
