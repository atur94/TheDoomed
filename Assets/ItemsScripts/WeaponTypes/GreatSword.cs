using UnityEngine;

[CreateAssetMenu(fileName = "New great sword", menuName = "Inventory System Items/Weapon/Great Sword")]
public class GreatSword : Weapon
{
    public override AttackType AttackType { get; } = AttackType.Melee;

    public override void Initialize()
    {
    }
}
