using System;
using UnityEngine;
using System.Collections;
using TMPro;

public class ItemDisplayControl : ScriptableObject
{
    public ItemSlot itemSlot;
    public GameObject displayInstance;
    public ItemStatsWindowBinder binder;

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
                foreach (var attribute in item.StatsEffects)
                {
                    if (attribute.FlatBonus != 0f && attribute.PercentBonus != 0f)
                    {
                        GameObject attributesPanel = Instantiate(binder.itemAttributesPrefab, binder.itemAttributes.transform);
                        TextMeshProUGUI[] attributePanel = attributesPanel.GetComponentsInChildren<TextMeshProUGUI>();
                        attributePanel[0].SetText(attribute.Name);
                        attributePanel[1].SetText(attribute.FlatBonus.ToString());
                        attributesPanel = Instantiate(binder.itemAttributesPrefab, binder.itemAttributes.transform);
                        attributePanel = attributesPanel.GetComponentsInChildren<TextMeshProUGUI>();
                        attributePanel[0].SetText("");
                        attributePanel[1].SetText(string.Format(attribute.PecentBonusFormat, attribute.PercentBonus));
                    }else if (attribute.PercentBonus == 0f)
                    {
                        GameObject attributesPanel = Instantiate(binder.itemAttributesPrefab, binder.itemAttributes.transform);
                        TextMeshProUGUI[] attributePanel = attributesPanel.GetComponentsInChildren<TextMeshProUGUI>();
                        attributePanel[0].SetText(attribute.Name);
                        attributePanel[1].SetText(attribute.FlatBonus.ToString());
                    }
                    else
                    {
                        GameObject attributesPanel = Instantiate(binder.itemAttributesPrefab, binder.itemAttributes.transform);
                        TextMeshProUGUI[] attributePanel = attributesPanel.GetComponentsInChildren<TextMeshProUGUI>();
                        attributePanel[0].SetText(attribute.Name);
                        attributePanel[1].SetText(string.Format(attribute.PecentBonusFormat, attribute.PercentBonus));
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

    public static ItemDisplayControl CreateItemDisplayControl(ItemSlot showSlot, GameObject displayPrefab, Transform showLayer)
    {
        ItemDisplayControl displayControl = ScriptableObject.CreateInstance<ItemDisplayControl>();
        displayControl.itemSlot = showSlot;
        displayControl.displayInstance = Instantiate(displayPrefab, showLayer);
        return displayControl;
    }

    private void OnEnable()
    {
    }
}
