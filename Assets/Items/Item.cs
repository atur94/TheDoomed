using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : Base
{
    public ItemType itemType;
    public GameObject itemModel;
    public Image itemImage;
    public ItemSlot CurrentItemSlot { get; set; }

    private void Awake()
    {
        Initialize();
    }
}


