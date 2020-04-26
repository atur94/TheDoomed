using UnityEngine;

public class Damagable : MonoBehaviour, IDamagable
{
    private float _health;
    public Item randomItem;

    private void Awake()
    {
        _health = 100;
    }

    public void TakeDamage(Damage damage)
    {
        var damageComponent = 20f + 0.2 * (damage.MagicalDamage + damage.PhysicalDamage);

        if (_health < 0)
        {
            DestroyThis();
        }
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
        if(randomItem != null)
            randomItem.SpawnItem(GetComponent<Collider>().transform.position);
    }
}
