
using System.Collections.Generic;

public class Damage
{
    public float PhysicalDamage { get; }
    public float MagicalDamage { get; }

    public List<Status> OnHitEffects;

    public Damage(float physicalDamage, float magicalDamage, List<Status> onHitEffects = null)
    {
        PhysicalDamage = physicalDamage;
        MagicalDamage = magicalDamage;
        this.OnHitEffects = onHitEffects;
    }
}