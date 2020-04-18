using UnityEngine;

[CreateAssetMenu(fileName = "New great sword", menuName = "Inventory System Items/Weapon/Great Sword")]
public class GreatSword : Weapon
{
    private AttackType _attackType = AttackType.Melee;

    public override AttackType AttackType => _attackType;

    public override void Initialize()
    {
    }
}
