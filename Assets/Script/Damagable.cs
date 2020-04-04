using System;
using UnityEngine;
using System.Collections;

public class Damagable : MonoBehaviour, IDamagable
{
    private float _health;
    public Item _randomItem;
    private Collider collider;

    private void Awake()
    {
        _health = 100;
//        _randomItem = ScriptableObject.CreateInstance<Weapon>();
    }

    public void DealDamage(Damage damage)
    {
        var damageComponent = 20f + 0.2 * (damage.MagicalDamage + damage.PhysicalDamage);

        if (_health < 0)
        {
            DestroyThis();
        }
    }

    private void Start()
    {
        collider = GetComponent<Collider>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DestroyThis();
        }
    }

    public void DestroyThis()
    {
        DropItem();
        Destroy(gameObject);
    }

    private void DropItem()
    {
        if(_randomItem != null)
            _randomItem.SpawnItem(collider.transform.position);
    }
}
