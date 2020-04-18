using UnityEngine;


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
        StatsEffects.Sort(Comparison);
    }



    private int Comparison(AttributeSet x, AttributeSet y)
    {
        if (x.order == y.order) return 0;
        if (x.order < y.order) return -1;
        return 1;
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
        var rb = spawnedItem.AddComponent<Rigidbody>();
        spawnedItem.gameObject.SetLayerRecursively(13);
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        
        rb.velocity = Vector3.up *4;


        rb.angularVelocity = Random.onUnitSphere * 4;
        Pickable pickable = spawnedItem.AddComponent<Pickable>();
        pickable._item = this;


    }


}


