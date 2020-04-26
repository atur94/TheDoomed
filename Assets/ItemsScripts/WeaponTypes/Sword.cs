using UnityEngine;

[CreateAssetMenu(fileName = "New sword", menuName = "Inventory System Items/Weapon/Sword")]
public class Sword : Weapon
{
    public override AttackType AttackType { get; } = AttackType.Melee;

    public override void Initialize()
    {
    }
}
