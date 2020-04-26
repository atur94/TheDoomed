using UnityEngine;

[CreateAssetMenu(fileName = "New staff", menuName = "Inventory System Items/Weapon/Staff")]
public class Staff : Weapon
{
    public override AttackType AttackType { get; } = AttackType.Range;

    public override void Initialize()
    {
    }
}
