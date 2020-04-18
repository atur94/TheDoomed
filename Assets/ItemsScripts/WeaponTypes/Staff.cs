using UnityEngine;

[CreateAssetMenu(fileName = "New staff", menuName = "Inventory System Items/Weapon/Staff")]
public class Staff : Weapon
{
    private AttackType _attackType = AttackType.Range;

    public override AttackType AttackType => _attackType;

    public override void Initialize()
    {
    }
}
