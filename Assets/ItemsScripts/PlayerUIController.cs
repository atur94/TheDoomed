using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public GameObject UI;
    public RectTransform Info;

    public Sprite spriteSlotLocked;
    public Sprite spriteSlotUnlocked;
    public Sprite spriteSlotUndefined;

    public GameObject weaponSlotUI;
    public GameObject backpackSlotUI;


    public GameObject itemSlotPrefab;
    public GameObject inventoryUI;

    public Character character;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            UI.SetActive(!UI.activeSelf);
        }

        if (character != null)
        {
            for (int i = 0; i < character.itemSlots.Count; i++)
            {
                character.itemSlots[i].UpdateSlotIcon();
            }

            for (int i = 0; i < character.inventory.inventorySlots.Count; i++)
            {
                character.inventory.inventorySlots[i].UpdateSlotIcon();
            }
        }

        _diff = Input.mousePosition - _lastCursorsPosition;
        _lastCursorsPosition = Input.mousePosition;
    }

    private Vector3 _diff;
    private Vector3 _lastCursorsPosition;

    public void OnInfoDrag()
    {
        Info.transform.position = Info.transform.position + _diff;
    }

    private void Build()
    {
        for (int i = 0; i < 24; i++)
        {
            GameObject itemSlotInstance = Instantiate(itemSlotPrefab, inventoryUI.transform);
            ItemSlot itemSlot = ItemSlot.CreateSlot(character, i, itemSlotInstance, spriteSlotLocked, spriteSlotUnlocked, spriteSlotUndefined);
            character.inventory.inventorySlots.Add(itemSlot);
        }
        ItemSlot.ConfigureEquipmentSlot(character.weaponSlot, weaponSlotUI, spriteSlotLocked, spriteSlotUnlocked, spriteSlotUndefined);
        ItemSlot.ConfigureEquipmentSlot(character.backpackSlot, backpackSlotUI, spriteSlotLocked, spriteSlotUnlocked, spriteSlotUndefined);
    }

    public void BindUIToCharacter(Character character)
    {
        this.character = character;
        Build();

    }

    

}
