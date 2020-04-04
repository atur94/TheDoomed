using System;
using UnityEngine;
using UnityEngine.UI;


public interface IPickable
{
    void Pick();
}

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

    public void SpawnItem(Vector3 location)
    {
        GameObject spawnedItem;
        if (itemModel == null)
        {
            spawnedItem= GameObject.CreatePrimitive(PrimitiveType.Cube);
            spawnedItem.transform.position = location;
            spawnedItem.transform.localScale = Vector3.one * 0.3f;
        }
        else
        {
            spawnedItem = Instantiate(itemModel, location, Quaternion.identity);
        }

        spawnedItem.layer = 13;
        spawnedItem.AddComponent<Rigidbody>();
        Pickable pickable = spawnedItem.AddComponent<Pickable>();
        pickable._item = this;


    }
}


