using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : Base
{
    public ItemType itemType;
    public GameObject itemModel;
    public Sprite itemSprite;
    public ItemSlot CurrentItemSlot { get; set; }

    private void Awake()
    {
        Initialize();
    }
}


