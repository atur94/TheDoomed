 
using System;
using TMPro;
using UnityEngine;

public class ItemSlot : ScriptableObject
{
    [SerializeField]
    public Item itemInSlot;

    public Character character { private get; set; }

    public Type itemTypeRestriction;

    public bool isMoving;

    public bool isHovering;
        
    public void PlaceItem(Item item)
    {
        if (itemInSlot == null)
        {
            if (CanBePlaced(item))
            {
                itemInSlot = item;
            }
        }
        else
        {
            if (CanBePlaced(item))
            {
                var tempSlot = item.CurrentItemSlot;
                var tempItem = itemInSlot;
                //Tutaj mogą być problemy z adresacją
                itemInSlot = item;
                tempSlot.itemInSlot = tempItem;
            }
        }
    }

    public bool CanBePlaced(Item item)
    {
        if (itemInSlot == null)
        {
            return CanBePlacedInternal(this, item);
        }
        return CanBePlacedInternal(this, item) && CanBePlacedInternal(itemInSlot.CurrentItemSlot, itemInSlot);
    }

    public static bool CanBePlacedInternal(ItemSlot itemSlot, Item item)
    {
        if (itemSlot.itemTypeRestriction == null)
        {
            return true;
        }


        if (itemSlot.itemTypeRestriction != null)
        {
            if(item is Equipment equipment)
            {
                if (itemSlot.itemTypeRestriction == equipment.GetType())
                {
                    if ((int)equipment.requiredLevel <= (int)itemSlot.character.Level)
                    {
                        return true;
                    }
                }
            }
        }
        
        return false;
    }





}