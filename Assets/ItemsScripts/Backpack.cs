using UnityEngine;

[CreateAssetMenu(fileName = "Backpack", menuName = "Inventory System System/Items/Backpack")]
public class Backpack : Equipment
{

    private AttributeSet _itemSlots;
    public AttributeSet ItemSlots
    {
        get { return _itemSlots; }
        set { _itemSlots = value; AddToList(value); }
    }

    public override void Initialize()
    {
        ItemSlots = new AttributeSet(AttributeType.StorageCapacity, 7, 0);
    }
}
