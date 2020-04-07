using System;
using UnityEngine;
using System.Collections;
using TMPro;

public class ItemDisplayControl : ScriptableObject
{
    public ItemSlot itemSlot;
    public GameObject displayInstance;
    public ItemStatsWindowBinder binder;
    public Character character;

    private void Awake()
    {
    }

    

    public void SetPosition(Vector3 position)
    {

    }

    public void DisplayFormat()
    {

    }

    public void Build()
    {
        if (binder == null)
        {
            binder = displayInstance.GetComponent<ItemStatsWindowBinder>();
        }

        if (binder != null && itemSlot.itemInSlot != null)
        {
            Item item = itemSlot.itemInSlot;
            binder.itemName.SetText(item.name);
            binder.typeValue.SetText(item.GetType().ToString());
            if (item is Equipment equipment)
            {
                binder.requiredLevel.SetText(equipment.requiredLevel.ToString());
                if (character.level < equipment.requiredLevel)
                {
                    binder.requiredLevel.color = Color.red;
                }
                foreach (var attribute in item.StatsEffects)
                {
                    string flat = attribute.FlatBonusAsString;
                    string percent = attribute.PecentBonusAsString;
                    if (Math.Abs(attribute.FlatBonus) > 0.0001f)
                    {
                        GameObject attributesPanel = Instantiate(binder.itemAttributesPrefab, binder.itemAttributes.transform);
                        TextMeshProUGUI[] attributePanel = attributesPanel.GetComponentsInChildren<TextMeshProUGUI>();
                        attributePanel[0].SetText(attribute.Name);
                        attributePanel[1].SetText(flat);
                    }
                    if (Math.Abs(attribute.PercentBonus) > 0.0001f)
                    {
                        GameObject attributesPanel = Instantiate(binder.itemAttributesPrefab, binder.itemAttributes.transform);
                        TextMeshProUGUI[] attributePanel = attributesPanel.GetComponentsInChildren<TextMeshProUGUI>();
                        attributePanel[0].SetText(attribute.Name);
                        attributePanel[1].SetText(percent);
                    }

                }
            }
            else
            {
                binder.requiredLevelParent.SetActive(false);
            }
            

        }
    }

    public void Destroy()
    {
        if (displayInstance != null)
        {
            Destroy(displayInstance);
        }
    }

    public static ItemDisplayControl CreateItemDisplayControl(ItemSlot showSlot, GameObject displayPrefab, Transform showLayer, Character character)
    {
        ItemDisplayControl displayControl = ScriptableObject.CreateInstance<ItemDisplayControl>();
        displayControl.itemSlot = showSlot;
        displayControl.displayInstance = Instantiate(displayPrefab, showLayer);
        displayControl.character = character;
        return displayControl;
    }

    private void OnEnable()
    {
    }
}
