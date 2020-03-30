 
using System;
using TMPro;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : ScriptableObject
{
    [SerializeField]
    public Item itemInSlot;

    public Character character { private get; set; }

    public int? slotNo;

    public GameObject itemSlotPrefab;

    public Image itemSlotImage;

    public Sprite spriteSlotLocked;
    public Sprite spriteSlotUnlocked;
    public Sprite spriteSlotUndefined;

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

    public Item LastItem;

    public void UpdateSlotIcon()
    {
        Backpack backpack = (Backpack)character.backpackSlot.itemInSlot;

        int itemSlots = backpack == null ? 0 : backpack.itemSlots;

        if (slotNo.HasValue)
        {
            if (slotNo.Value < itemSlots)
            {
                itemSlotImage.color = new Color(1,1,1,0);
            }
            else
            {
                itemSlotImage.sprite = spriteSlotLocked;
                itemSlotImage.color = new Color(1,1,1,1);
            }
        }
        else
        {
            if (itemInSlot == null)
            {
                itemSlotImage.color = new Color(1,1,1,0);
            }
            else
            {
                if (itemInSlot.itemSprite == null)
                {
                    itemSlotImage.color = new Color(1, 1, 1, 1);

                    itemSlotImage.sprite = spriteSlotUndefined;
                }
                else
                {
                    itemSlotImage.color = new Color(1, 1, 1, 1);

                    itemSlotImage.sprite = itemInSlot.itemSprite;
                }
            }
        }



        LastItem = itemInSlot;
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

    public void Update()
    {

    }

    public void DropItem()
    {
        Debug.Log("Item droped");
    }

    public static ItemSlot CreateSlot(Character character, Type itemRestriction = null)
    {
        ItemSlot slot = ScriptableObject.CreateInstance<ItemSlot>();
        slot.character = character;
        slot.itemTypeRestriction = itemRestriction;
        return slot;
    }

    public static ItemSlot CreateSlot(Character character, int slotNo,GameObject slotInstance, Sprite spriteLocked,
        Sprite spriteUnlocked, Sprite spriteUndefined, Type itemRestriction = null)
    {
        ItemSlot itemSlot = ScriptableObject.CreateInstance<ItemSlot>();
        itemSlot.slotNo = slotNo;
        itemSlot.character = character;
        itemSlot.itemSlotPrefab = slotInstance;
        itemSlot.spriteSlotLocked = spriteLocked;
        itemSlot.spriteSlotUnlocked = spriteUnlocked;
        itemSlot.spriteSlotUndefined = spriteUndefined;
        itemSlot.itemTypeRestriction = itemRestriction;
        Image[] images = slotInstance.GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            if (image.tag.Equals("ItemSlotInside"))
            {
                itemSlot.itemSlotImage = image;
            }
        }
        return itemSlot;
    }

    public static void ConfigureEquipmentSlot(ItemSlot itemSlot, GameObject slotInstance, Sprite spriteLocked,
        Sprite spriteUnlocked, Sprite spriteUndefined)
    {
        itemSlot.itemSlotPrefab = slotInstance;
        itemSlot.spriteSlotLocked = spriteLocked;
        itemSlot.spriteSlotUnlocked = spriteUnlocked;
        itemSlot.spriteSlotUndefined = spriteUndefined;
        Image[] images = slotInstance.GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            if (image.tag.Equals("ItemSlotInside"))
            {
                itemSlot.itemSlotImage = image;
            }
        }

    }
}