using UnityEngine;

[CreateAssetMenu(fileName = "New bow", menuName = "Inventory System Items/Weapon/Bow")]
public class Bow : Weapon
{
    private AttackType _attackType = AttackType.Range;

    public override AttackType AttackType => _attackType;

    public override void Initialize()
    {
    }
}
