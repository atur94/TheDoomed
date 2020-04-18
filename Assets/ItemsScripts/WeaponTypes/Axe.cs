using UnityEngine;

[CreateAssetMenu(fileName = "New axe", menuName = "Inventory System Items/Weapon/Spear")]
public class Axe : Weapon
{
    private AttackType _attackType = AttackType.Melee;

    public override AttackType AttackType => _attackType;

    public override void Initialize()
    {
    }
}
