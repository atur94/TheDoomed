using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Inventory : ScriptableObject
{
    public ItemSlot backpackSlot;
    public List<ItemSlot> inventorySlots;
    public Character character;
    private int _inventoryMaxCapacity = 24;
    private void Awake()
    {
        inventorySlots = new List<ItemSlot>(_inventoryMaxCapacity);
    }

    public void Initialize()
    {

    }

    public void PutItemToInventory(Item item)
    {
        if (_backpack == null) return;
        for (int i = 0; i < (int)_backpack.ItemSlots.FlatBonus; i++)
        {
            ItemSlot currentSlot = inventorySlots[i];
            if (currentSlot.itemInSlot == null)
            {
                currentSlot.itemInSlot = item;
                currentSlot.slotNo = i;
                return;
            }
        }
        DropItem(item);

    }

    public void DropItem(Item item)
    {
        item.SpawnItem(character.transform.position);
    }

    public void DropItem(ItemSlot slot)
    {
        if (slot.itemInSlot == null) return;
        DropItem(slot.itemInSlot);
        slot.itemInSlot = null;
    }

    private Backpack _backpack;
    private Backpack _lastBackpack;

    public void UpdateInventory()
    {
        _backpack = (Backpack)backpackSlot.itemInSlot;
        int lastSlots = _lastBackpack == null ? 0 : (int)_lastBackpack.ItemSlots.FlatBonus;
        int currentSlots = _backpack == null ? 0 : (int)_backpack.ItemSlots.FlatBonus;


        int slotsDiffrence = currentSlots - lastSlots;

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].UpdateSlot();
        }

        _lastBackpack = _backpack;
    }

    public static Inventory CreateInventory(Character character)
    {
        Inventory inventory = ScriptableObject.CreateInstance<Inventory>();
        inventory.backpackSlot = character.backpackSlot;
        inventory.character = character;
        return inventory;
    }
}

