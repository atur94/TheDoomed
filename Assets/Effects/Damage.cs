
using System.Collections.Generic;

public class Damage
{
    public float PhysicalDamage { get; }
    public float MagicalDamage { get; }

    private List<Status> Effects;

    public Damage(float physicalDamage, float magicalDamage, List<Status> effects = null)
    {
        PhysicalDamage = physicalDamage;
        MagicalDamage = magicalDamage;
        Effects = effects;
    }
}