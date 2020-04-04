using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public abstract partial class CharacterBase : MonoBehaviour
{
    public float Level;
    public int Id;
    protected int MaxLevel = 25;
    public Attributes BaseAttributes;


    public float currentHealth;

    public float currentMana;

    protected void Initialize(Character character)
    {
        InitializeList(character);
    }
}

public class CommonAttribute
{
    protected Character Character;

    protected float _base;
    public virtual float Base
    {
        protected get =>
            _base + StrengthCoef * Character.strength.Value + VitalityCoef * Character.vitality.Value +
            AgilityCoef * Character.agility.Value + IntelligenceCoef * Character.intelligence.Value + PerLevel * Character.Level;
        set => _base = value;
    }

    [HideInInspector]
    public float StrengthCoef;
    [HideInInspector]
    public float VitalityCoef;
    [HideInInspector]
    public float AgilityCoef;
    [HideInInspector]
    public float IntelligenceCoef;
    [HideInInspector]
    public AttributeType AttributeType;
    [HideInInspector]
    public float PerLevel = 0f;

    private object statLock = new object();
    public float FlatBonuses
    {
        get
        {
            float sum = 0;
            for (int i = 0; i < Character.itemSlots.Count; i++)
            {
                Base item = Character.itemSlots[i].itemInSlot;
                if (item != null)
                {
                    Parallel.For(0, item.StatsEffects.Count, j =>
                    {
                        AttributeSet attribute = item.StatsEffects[j];
                        if (attribute.AttributeType == AttributeType)
                        {
                            lock (statLock)
                            {
                                sum += attribute.FlatBonus;
                            }
                        }
                    });
                }
            }

            return sum;
        }
    }

    public float PercentBonuses
    {
        get
        {
            float sum = 0;
            for (int i = 0; i < Character.itemSlots.Count; i++)
            {
                Base item = Character.itemSlots[i].itemInSlot;
                if (item != null)
                {
                    Parallel.For(0, item.StatsEffects.Count, j =>
                    {
                        AttributeSet attribute = item.StatsEffects[j];
                        if (attribute.AttributeType == AttributeType)
                        {
                            lock (statLock)
                            {
                                sum += attribute.PercentBonus;
                            }
                        }
                    });

                }
            }
            return sum;
        }
    }

    public virtual float Value => (Base + FlatBonuses) * (1 + PercentBonuses);


    public CommonAttribute(float baseAttribute, float vitalityCoef, float strengthCoef, float agilityCoef,
        float intelligenceCoef, float perLevel, Character character)
    {
        Character = character;
        _base = baseAttribute;
        VitalityCoef = vitalityCoef;
        StrengthCoef = strengthCoef;
        AgilityCoef = agilityCoef;
        IntelligenceCoef = intelligenceCoef;
        PerLevel = perLevel;
    }

    public CommonAttribute(InitialAttribute attribute, Character character, AttributeType attributeType)
    {
        Character = character;
        _base = attribute.startingValue;
        VitalityCoef = attribute.vitalityCoef;
        StrengthCoef = attribute.strengthCoef;
        AgilityCoef = attribute.agilityCoef;
        IntelligenceCoef = attribute.intelligenceCoef;
        PerLevel = attribute.perLevel;
        AttributeType = attributeType;
    }
}

public class MainAttribute : CommonAttribute
{
    public float Added;

    public override float Base
    {
        protected get => _base + Added + PerLevel * Character.Level;
        set => _base = value;
    }

    public override float Value => (int)((Base + FlatBonuses) * (1 + PercentBonuses));

    public MainAttribute(float baseAttribute, float perLevel, Character character, AttributeType attributeType) : base(baseAttribute, 0f, 0f, 0f, 0f, perLevel, character)
    {
    }

    public MainAttribute(InitialAttribute attribute, Character character, AttributeType attributeType) : base(attribute, character, attributeType)
    {
    }
}

public class TimeAttribute : CommonAttribute
{
    public override float Value => Base + PercentBonuses;

    public TimeAttribute(float baseAttribute, float vitalityCoef, float strengthCoef, float agilityCoef, float intelligenceCoef, float perLevel, Character character) : base(baseAttribute, vitalityCoef, strengthCoef, agilityCoef, intelligenceCoef, perLevel, character)
    {
    }

    public TimeAttribute(InitialAttribute attribute, Character character, AttributeType attributeType) : base(attribute, character, attributeType)
    {
    }
}

public class AttackSpeedAttribute : CommonAttribute
{
    public override float Base
    {
        protected get
        {
            return _base * (1 + StrengthCoef * Character.strength.Value + VitalityCoef * Character.vitality.Value +
                            AgilityCoef * Character.agility.Value + IntelligenceCoef * Character.intelligence.Value +
                            PerLevel * Character.Level);
        }
        set { _base = value; }
    }

    public override float Value => Base * (1 + PercentBonuses);

    public AttackSpeedAttribute(float baseAttribute, float vitalityCoef, float strengthCoef, float agilityCoef, float intelligenceCoef, float perLevel, Character character) : base(baseAttribute, vitalityCoef, strengthCoef, agilityCoef, intelligenceCoef, perLevel, character)
    {
    }

    public AttackSpeedAttribute(InitialAttribute attribute, Character character, AttributeType attributeType) : base(attribute, character, attributeType)
    {
    }
}


[Serializable]
public struct InitialAttribute
{
    public float startingValue;
    public float vitalityCoef;
    public float strengthCoef;
    public float agilityCoef;
    public float intelligenceCoef;
    public float perLevel;
}


[Serializable]
public struct Attributes
{
    public InitialAttribute physicalAttack;
    public InitialAttribute magicPower;
    public InitialAttribute health;
    public InitialAttribute mana;
    public InitialAttribute physicalDefense;
    public InitialAttribute magicalDefense;
    public InitialAttribute attackSpeed;
    public InitialAttribute channelingTime;
    public InitialAttribute castingTime;
    public InitialAttribute cooldownReduction;
    public InitialAttribute vitality;
    public InitialAttribute strength;
    public InitialAttribute agility;
    public InitialAttribute intelligence;
    public InitialAttribute criticalChance;
    public InitialAttribute criticalDamage;
    public InitialAttribute evasion;
    public InitialAttribute accuracy;
    public InitialAttribute movementSpeed;
    public InitialAttribute attackRange;
    public InitialAttribute healthRegen;
    public InitialAttribute manaRegen;
    public InitialAttribute fieldOfViewAngle;
    public InitialAttribute fieldOfViewRangeLight;
    public InitialAttribute fieldOfViewRangeDark;
    public InitialAttribute healthPerLevel;
    public InitialAttribute manaPerLevel;
    public InitialAttribute turnRate;
}