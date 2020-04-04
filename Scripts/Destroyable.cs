using UnityEngine;

public class Destroyable : MonoBehaviour, IDamagable
{
    private float _health;
    private Item _randomItem;


    private void Awake()
    {
        _health = 100;
    }

    public void DealDamage(Damage damage)
    {
        var damageOutput = damage.MagicalDamage + damage.PhysicalDamage;
        _health -= 20 + damageOutput * 0.2f;
        if (_health < 0)
        {
            DestroyImplemented();
        }
    }

    public void 

    public void DestroyImplemented()
    {
        DropItem();
        Destroy(this);
    }
}
