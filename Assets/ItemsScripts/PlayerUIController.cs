using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public GameObject UI;
    public RectTransform Info;

    public Sprite spriteSlotLocked;
    public Sprite spriteSlotUnlocked;
    public Sprite spriteSlotUndefined;

    public GameObject backpackSlotUI;
    public GameObject weaponSlotUI;
    public GameObject headSlotUI;
    public GameObject chestSlotUI;
    public GameObject legsSlotUI;
    public GameObject necklaceSlotUI;
    public GameObject bootsSlotUI;
    public GameObject ringSlotUI;
    public GameObject orbSlotUI;


    public GameObject itemSlotPrefab;
    public GameObject inventoryUI;

    public Character character;

    private GameObject _movable;
    private ItemSlot _selectedItemSlot;
    private ItemSlot _sourceItemSlot;

    public Transform moveLayerParent;

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
                character.itemSlots[i].UpdateSlot();
            }


            character.inventory.UpdateInventory();

            if (_movable != null)
            {
                _movable.transform.position = Input.mousePosition;
            }

            if (_selectedItemSlot != null)
            {
                _selectedItemSlot.UpdateSlot();
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
            ItemSlot itemSlot = ItemSlot.CreateSlot(character, i, itemSlotInstance);
            character.inventory.inventorySlots.Add(itemSlot);
        }

        ItemSlot.ConfigureEquipmentSlot(character.headSlot, headSlotUI);
        ItemSlot.ConfigureEquipmentSlot(character.chestSlot, chestSlotUI);
        ItemSlot.ConfigureEquipmentSlot(character.legsSlot, legsSlotUI);
        ItemSlot.ConfigureEquipmentSlot(character.weaponSlot, weaponSlotUI);
        ItemSlot.ConfigureEquipmentSlot(character.necklaceSlot, necklaceSlotUI);
        ItemSlot.ConfigureEquipmentSlot(character.bootsSlot, bootsSlotUI);
        ItemSlot.ConfigureEquipmentSlot(character.ringSlot, ringSlotUI);
        ItemSlot.ConfigureEquipmentSlot(character.orbSlot, orbSlotUI);
        ItemSlot.ConfigureEquipmentSlot(character.backpackSlot, backpackSlotUI);

    }

    public void BindUIToCharacter(Character character)
    {
        this.character = character;
        Build();

    }

    public void ItemSlotEventHandler(ItemSlot slot, EventTriggerType evntType)
    {
        switch (evntType)
        {
            case EventTriggerType.PointerUp:
                Debug.Log($"UP {slot.slotNo}");
                MoveFloatingSlot(slot);
                break;
            case EventTriggerType.BeginDrag:
                Debug.Log($"Begin {slot.slotNo}");
                CreateFloatingSlot(slot);
                break;
            case EventTriggerType.EndDrag:
                Debug.Log($"End {slot.slotNo}");
                DestroyFloatingSlot(_selectedItemSlot);
                break;
        }
    }

    private void CreateFloatingSlot(ItemSlot sourceItemSlot)
    {
        if (sourceItemSlot.isLocked || _selectedItemSlot != null || sourceItemSlot.itemInSlot == null) return;

        _selectedItemSlot = sourceItemSlot.Copy();
        _sourceItemSlot = sourceItemSlot;
        _selectedItemSlot.character = character;
        _sourceItemSlot.isSelected = true;
        _movable = _selectedItemSlot.itemSlotInstance = Instantiate(itemSlotPrefab, moveLayerParent.transform);
        _movable.GetComponent<RectTransform>().pivot = new Vector2(0.5f,0.5f);
        _selectedItemSlot.itemSlotInstance.transform.SetParent(moveLayerParent);
        CanvasGroup  group = _movable.AddComponent<CanvasGroup>();
        group.blocksRaycasts = false;
        group.interactable = false;
        _movable.name = "Moving";
        _movable.transform.position = sourceItemSlot.itemSlotInstance.transform.position;

    }

    private void MoveFloatingSlot(ItemSlot targetSlot)
    {
        if (_selectedItemSlot == null) return;
        if (targetSlot.isLocked) {
            DestroyFloatingSlot(_selectedItemSlot);
            return;
        }

        if (targetSlot.CanBePlaced(_sourceItemSlot.itemInSlot))
        {
            Item tempItem = targetSlot.itemInSlot;
            targetSlot.itemInSlot = _sourceItemSlot.itemInSlot;
            _sourceItemSlot.itemInSlot = tempItem;
        }

        DestroyFloatingSlot(_selectedItemSlot);
    }

    private void DestroyFloatingSlot(ItemSlot slot)
    {
        if (_movable != null && _selectedItemSlot != null)
        {
            _sourceItemSlot.isSelected = false;
            _sourceItemSlot = null;
            _selectedItemSlot = null;
            _sourceItemSlot = null;
            Destroy(slot.itemSlotInstance.gameObject);
        }
    }

}
