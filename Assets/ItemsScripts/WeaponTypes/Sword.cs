using UnityEngine;

[CreateAssetMenu(fileName = "New sword", menuName = "Inventory System Items/Weapon/Sword")]
public class Sword : Weapon
{
    private AttackType _attackType = AttackType.Melee;

    public override AttackType AttackType => _attackType;

    public override void Initialize()
    {
    }
}
