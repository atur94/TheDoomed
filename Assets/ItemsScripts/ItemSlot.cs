 
using System;
using TMPro;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class ItemSlot : ScriptableObject
{
    [SerializeField]
    public Item itemInSlot;

    public Character character { private get; set; }

    public int? slotNo;

    public GameObject itemSlotInstance
    {
        get => _itemSlotInstance;
        set
        {
            _itemSlotInstance = value;
            if (isMoving) return;
            if (_itemSlotInstance != null)
            {
                SlotEventsConfiguration();
            }
        }
    }

    private PlayerUIController _uiController;

    public void Awake()
    {
        _uiController = FindObjectOfType<PlayerUIController>();
    }

    private Image _itemSlotImage;
    public Image itemSlotImage
    {
        get
        {
            if (_itemSlotImage == null && itemSlotInstance != null)
            {
                Image[] images = itemSlotInstance.GetComponentsInChildren<Image>();
                foreach (var image in images)
                {
                    if (image.tag.Equals("ItemSlotInside"))
                    {
                        _itemSlotImage = image;
                    }
                }
            }

            return _itemSlotImage;
        }
    }


    public void PlaceItemInEquipment(Item item)
    {
        float hpPercentage = character.currentHealth / character.health.Value;
        float mpPercentage = character.currentMana / character.mana.Value;
        itemInSlot = item;

        if (!slotNo.HasValue)
        {
            if(Math.Abs(hpPercentage - character.currentHealth / character.health.Value) > 0.005f)
                character.currentHealth = (hpPercentage > 0.04f ? hpPercentage - 0.04f : 0.04f) * character.health.Value;

            if (Math.Abs(hpPercentage - character.currentMana / character.mana.Value) > 0.005f)
                character.currentMana = (mpPercentage > 0.04f ? mpPercentage - 0.04f : 0.04f) * character.mana.Value;
        }
    }

    private Sprite SpriteSlotLocked => _uiController.spriteSlotLocked;
    private Sprite SpriteSlotUnlocked => _uiController.spriteSlotUnlocked;
    private Sprite SpriteSlotUndefined => _uiController.spriteSlotUndefined;

    public Type itemTypeRestriction;

    public bool isMoving;
    public bool isSelected;
    public bool isHovering;

    public bool isLocked = false;
        
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
    private GameObject _itemSlotInstance;

    public void DropItem()
    {
        if (isLocked && itemInSlot != null)
        {
            itemInSlot.SpawnItem(character.transform.position);
            itemInSlot = null;
        }
    }

    private void SetItemSlotVisibility(bool locked = false)
    {
        Sprite sprite;
        float alphaWhileSelected = isSelected ? 0.5f : 1f;
        if(!locked)
        {
            if (itemInSlot == null)
            {
                sprite = null;
                itemSlotImage.color = new Color(1, 1, 1, 0);
            }
            else if (itemInSlot.itemSprite != null)
            {
                sprite = itemInSlot.itemSprite;
                itemSlotImage.color = new Color(1, 1, 1, alphaWhileSelected);

            }
            else
            {
                sprite = SpriteSlotUndefined;
                itemSlotImage.color = new Color(1, 1, 1, alphaWhileSelected);

            }
        }
        else
        {
            sprite = SpriteSlotLocked;
            itemSlotImage.color = new Color(1, 1, 1, alphaWhileSelected);
        }

        itemSlotImage.sprite = sprite;
    }


    public void UpdateSlot()
    {
        Backpack backpack = (Backpack)character.backpackSlot.itemInSlot;

        int itemSlots = backpack == null ? 0 : (int)backpack.ItemSlots.FlatBonus;

        if (itemInSlot != null) itemInSlot.CurrentItemSlot = this;

        if (slotNo.HasValue)
        {
            if (slotNo.Value < itemSlots)
            {
                isLocked = false;
                SetItemSlotVisibility();
            }
            else
            {
                isLocked = true;
                SetItemSlotVisibility(true);
            }
        }
        else
        {
            isLocked = false;
            SetItemSlotVisibility();
        }
        DropItem();
        LastItem = itemInSlot;
    }

    public bool CanBePlaced(ItemSlot sourceItemSlot)
    {
        return CanBePlaced(sourceItemSlot.itemInSlot);
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
                    if ((int)equipment.requiredLevel <= (int)itemSlot.character.level)
                    {
                        return true;
                    }
                }
            }
        }
        
        return false;
    }

    public ItemSlot Copy()
    {
        ItemSlot copy = ScriptableObject.CreateInstance<ItemSlot>();
        copy.itemInSlot = itemInSlot;
        copy.itemInSlot.itemSprite = itemInSlot.itemSprite; 
        return copy;
    }

    public static ItemSlot CreateSlot(Character character, Type itemRestriction = null)
    {
        ItemSlot slot = ScriptableObject.CreateInstance<ItemSlot>();
        slot.character = character;
        slot.itemTypeRestriction = itemRestriction;
        return slot;
    }

    public static ItemSlot CreateSlot(Character character, int slotNo,GameObject slotInstance, Type itemRestriction = null)
    {
        ItemSlot itemSlot = ScriptableObject.CreateInstance<ItemSlot>();
        itemSlot.slotNo = slotNo;
        itemSlot.character = character;
        itemSlot.itemSlotInstance = slotInstance;
        itemSlot.itemTypeRestriction = itemRestriction;
        return itemSlot;
    }

    public static void ConfigureEquipmentSlot(ItemSlot itemSlot, GameObject slotInstance)
    {
        itemSlot.itemSlotInstance = slotInstance;
    }

    public static void OnDragInit(ItemSlot itemSlot, Vector3 position)
    {
    }

}