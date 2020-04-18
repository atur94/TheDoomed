using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour, IDropHandler
{
    public GameObject UI;
    public RectTransform Info;

    public GameObject displayAttributesPrefab;


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
    private ItemSlot _itemSlotOnHover;
    private ItemDisplayControl _displayControl;

    public Transform moveLayerParent;

    private UIAttributeBinder _uiAttributeBinder;

    void EnableButtons()
    {
        _uiAttributeBinder.addVitality.gameObject.SetActive(true);
        _uiAttributeBinder.addAgility.gameObject.SetActive(true);
        _uiAttributeBinder.addStrength.gameObject.SetActive(true);
        _uiAttributeBinder.addIntelligence.gameObject.SetActive(true);
    }

    void DisableButtons()
    {
        _uiAttributeBinder.addVitality.gameObject.SetActive(false);
        _uiAttributeBinder.addAgility.gameObject.SetActive(false);
        _uiAttributeBinder.addStrength.gameObject.SetActive(false);
        _uiAttributeBinder.addIntelligence.gameObject.SetActive(false);
    }

    void Start()
    {
        EventTrigger ent = UI.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry onMouseUp = new EventTrigger.Entry();
        onMouseUp.eventID = EventTriggerType.Drop;
        onMouseUp.callback.AddListener(DropItem);
        ent.triggers.Add(onMouseUp);
        _uiAttributeBinder = UI.GetComponentInChildren<UIAttributeBinder>();
        DisableButtons();
        _uiAttributeBinder.addVitality.onClick.AddListener(() =>
        {
            if (character.AddStatPoint(AttributeType.Vitality) <= 0)
            {
                DisableButtons();
            }
        });
        _uiAttributeBinder.addStrength.onClick.AddListener(() =>
        {
            if (character.AddStatPoint(AttributeType.Strength) <= 0)
            {
                DisableButtons();
            }
        });
        _uiAttributeBinder.addAgility.onClick.AddListener(() =>
        {
            if (character.AddStatPoint(AttributeType.Agility) <= 0)
            {
                DisableButtons();
            }

        });
        _uiAttributeBinder.addIntelligence.onClick.AddListener(() =>
        {
            if (character.AddStatPoint(AttributeType.Intelligence) <= 0)
            {
                DisableButtons();
            }
        });
    }

    public void DropItem(BaseEventData arg0)
    {
        if (_selectedItemSlot != null && _sourceItemSlot != null)
        {
            character.inventory.DropItem(_sourceItemSlot);
        }
    }

    void UpdateAttributesUI()
    {
        if (_uiAttributeBinder == null && !UI.activeSelf) return;
        if (character.PointsForDistribution > 0)
        {
            EnableButtons();
        }
        _uiAttributeBinder.distributionPoints.SetText(character.PointsForDistribution.ToString());
        _uiAttributeBinder.level.SetText(character.level.ToString());
        _uiAttributeBinder.vitality.SetText(character.vitality.Value.ToString());
        _uiAttributeBinder.strength.SetText(character.strength.Value.ToString());
        _uiAttributeBinder.agility.SetText(character.agility.Value.ToString());
        _uiAttributeBinder.intelligence.SetText(character.intelligence.Value.ToString());
        _uiAttributeBinder.physicalDamage.SetText(character.physicalAttack.Value.ToString());
        _uiAttributeBinder.magicalDamage.SetText(character.magicPower.Value.ToString());
        _uiAttributeBinder.physicalDefense.SetText(character.physicalDefense.Value.ToString());
        _uiAttributeBinder.magicalDefense.SetText(character.magicalDefense.Value.ToString());
        _uiAttributeBinder.attackRate.SetText(character.attackSpeed.Value.ToString());
        _uiAttributeBinder.movementSpeed.SetText(character.movementSpeed.Value.ToString());
    }

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

            if (_displayControl != null)
            {
                _displayControl.displayInstance.GetComponent<RectTransform>().position = Input.mousePosition;
            }
        }
        _diff = Input.mousePosition - _lastCursorsPosition;
        _lastCursorsPosition = Input.mousePosition;
    }

    private void LateUpdate()
    {
        UpdateAttributesUI();
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
                MoveFloatingSlotInInventory(slot);
                break;
            case EventTriggerType.BeginDrag:
                CreateFloatingSlot(slot);
                break;
            case EventTriggerType.EndDrag:
                DestroyFloatingSlot(_selectedItemSlot);
                break;
            case EventTriggerType.PointerEnter:
                if(_displayControl != null) 
                    _displayControl.Destroy();
                _displayControl = null;
                if (slot.itemInSlot != null)
                {
                    _itemSlotOnHover = slot;
                    _displayControl = ItemDisplayControl.CreateItemDisplayControl(slot, displayAttributesPrefab, moveLayerParent, character);
                    _displayControl.Build();
                }
                break;
            case EventTriggerType.PointerExit:
                if(_displayControl != null)
                {
                    _displayControl.Destroy();
                    _displayControl = null;
                }
                _itemSlotOnHover = null;
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
        _movable.layer = 14;
        _selectedItemSlot.itemSlotInstance.transform.SetParent(moveLayerParent);
        CanvasGroup  group = _movable.AddComponent<CanvasGroup>();
        group.blocksRaycasts = false;
        group.interactable = false;
        group.ignoreParentGroups = false;
        _movable.name = "Moving";
        _movable.transform.position = sourceItemSlot.itemSlotInstance.transform.position;

    }

    private void MoveFloatingSlotInInventory(ItemSlot targetSlot)
    {
        if (_selectedItemSlot == null) return;
        if (targetSlot.isLocked) {
            DestroyFloatingSlot(_selectedItemSlot);
            return;
        }

        if (targetSlot.CanBePlaced(_sourceItemSlot.itemInSlot))
        {
            float hpPercentage = character.currentHealth / character.health.Value;
            Item tempItem = targetSlot.itemInSlot;
            targetSlot.PlaceItemInEquipment(_sourceItemSlot.itemInSlot);
            _sourceItemSlot.itemInSlot = tempItem;
            character.currentHealth = hpPercentage * character.health.Value;
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

        if (_displayControl != null)
        {
            _displayControl.Destroy();
            _displayControl = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("TETETETETE");
    }
}
