using UnityEngine;

[CreateAssetMenu(fileName = "New bow", menuName = "Inventory System Items/Weapon/Bow")]
public class Bow : RangeWeaponBase
{
    public override AttackType AttackType { get; } = AttackType.Range;

    public override void Initialize()
    {
    }
}
