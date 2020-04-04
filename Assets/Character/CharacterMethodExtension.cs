using UnityEngine;
public abstract partial class Character
{
    public void Collect(Pickable pickable)
    {
        foreach (var eqSlot in itemSlots)
        {
            if (eqSlot != null && eqSlot.itemInSlot == null && eqSlot.itemTypeRestriction == pickable._item.GetType() && eqSlot.CanBePlaced(pickable._item))
            {
                eqSlot.itemInSlot = pickable._item;
                return;
            }
        }
        inventory.PutItemToInventory(pickable._item);
    }
}