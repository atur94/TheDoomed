public abstract class Equipment : Item
{
    public string category = "Equipment";
    public float requiredLevel = 0;

    public bool CanBePlacedInSlot(ItemSlot eqSlot)
    {
        if (eqSlot != null)
        {
            if (eqSlot.itemTypeRestriction == null)
            {
                return true;
            }

            return GetType().IsSubclassOf(eqSlot.itemTypeRestriction) || GetType() == eqSlot.itemTypeRestriction;
        }

        return false;
    }

}



