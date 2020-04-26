
using System.Collections.Generic;

public class Damage
{
    public float PhysicalDamage { get; }
    public float MagicalDamage { get; }
    public Character damageDealer { get; }

    public List<Status> OnHitEffects;

    public Damage(float physicalDamage, float magicalDamage, Character dealer, List<Status> onHitEffects = null)
    {
        PhysicalDamage = physicalDamage;
        MagicalDamage = magicalDamage;
        damageDealer = dealer;
        this.OnHitEffects = onHitEffects;
    }
}