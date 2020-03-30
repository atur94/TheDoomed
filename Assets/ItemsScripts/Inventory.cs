using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Inventory : ScriptableObject
{
    public ItemSlot backpackSlot;
    public List<ItemSlot> inventorySlots;
    private int _inventoryMaxCapacity = 24;
    private void Awake()
    {
        inventorySlots = new List<ItemSlot>(_inventoryMaxCapacity);
    }

    public void Initialize()
    {

    }

    private Backpack _backpack;
    private Backpack _lastBackpack;
    public void UpdateInventory()
    {
        _backpack = (Backpack)backpackSlot.itemInSlot;
        if (_backpack == _lastBackpack) return;
        int lastSlots = _lastBackpack == null ? 0 : _lastBackpack.itemSlots;
        int currentSlots = _backpack == null ? 0 : _backpack.itemSlots;


        int slotsDiffrence = currentSlots - lastSlots;
        Debug.Log($"Changing slot numbers from {lastSlots} to {_backpack.itemSlots}");

        if (slotsDiffrence < 0)
        {
            for (int i = lastSlots - 1; i >= currentSlots; i--)
            {
            Debug.Log($"Dropping slot number {i}");

            }
        }

        _lastBackpack = _backpack;
    }

    public static Inventory CreateInventory(Character character)
    {
        Inventory inventory = ScriptableObject.CreateInstance<Inventory>();
        inventory.backpackSlot = character.backpackSlot;
        return inventory;
    }



}

