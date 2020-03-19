using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract partial class CharacterBase : MonoBehaviour
{
    public float Level;
    public int Id;
    protected int MaxLevel = 25;
    public Attributes BaseAttributes;

    public void Start()
    {
    }

    protected void Initialize()
    {
        InitializeList();

    }
}

public class CommonAttribute
{
    public CharacterBase Character;

    protected float _base;
    public virtual float Base
    {
        protected get =>
            _base + StrengthCoef * Character.strength.Value + VitalityCoef * Character.vitality.Value +
            AgilityCoef * Character.agility.Value + IntelligenceCoef * Character.intelligence.Value + PerLevel * Character.Level;
        set => _base = value;
    }

    public float StrengthCoef;
    public float VitalityCoef;
    public float AgilityCoef;
    public float IntelligenceCoef;

    public List<float> FlatBonusesList = new List<float>();
    public List<float> PercentBonusesList = new List<float>();

    public float PerLevel = 0f;

    public float FlatBonuses
    {
        get
        {
            float sum = 0;
            for (int i = 0; i < FlatBonusesList.Count; i++)
            {
                sum += FlatBonusesList[i];
            }

            return sum;
        }
    }

    public float PercentBonuses
    {
        get
        {
            float sum = 0;
            for (int i = 0; i < PercentBonusesList.Count; i++)
            {
                sum += PercentBonusesList[i];
            }

            return sum;
        }
    }

    public virtual float Value => (Base + FlatBonuses) * (1 + PercentBonuses);


    public CommonAttribute(float baseAttribute, float vitalityCoef, float strengthCoef, float agilityCoef,
        float intelligenceCoef, float perLevel, CharacterBase character)
    {
        Character = character;
        _base = baseAttribute;
        VitalityCoef = vitalityCoef;
        StrengthCoef = strengthCoef;
        AgilityCoef = agilityCoef;
        IntelligenceCoef = intelligenceCoef;
        PerLevel = perLevel;
    }

    public CommonAttribute(InitialAttribute attribute, CharacterBase character)
    {
        Character = character;
        _base = attribute.startingValue;
        VitalityCoef = attribute.vitalityCoef;
        StrengthCoef = attribute.strengthCoef;
        AgilityCoef = attribute.agilityCoef;
        IntelligenceCoef = attribute.intelligenceCoef;
        PerLevel = attribute.perLevel;
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

    public MainAttribute(float baseAttribute, float perLevel, CharacterBase character) : base(baseAttribute, 0f, 0f, 0f, 0f, perLevel, character)
    {
    }

    public MainAttribute(InitialAttribute attribute, CharacterBase character) : base(attribute, character)
    {
    }
}

public class TimeAttribute : CommonAttribute
{
    public override float Value => Base + PercentBonuses;

    public TimeAttribute(float baseAttribute, float vitalityCoef, float strengthCoef, float agilityCoef, float intelligenceCoef, float perLevel, CharacterBase character) : base(baseAttribute, vitalityCoef, strengthCoef, agilityCoef, intelligenceCoef, perLevel, character)
    {
    }

    public TimeAttribute(InitialAttribute attribute, CharacterBase character) : base(attribute, character)
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

    public AttackSpeedAttribute(float baseAttribute, float vitalityCoef, float strengthCoef, float agilityCoef, float intelligenceCoef, float perLevel, CharacterBase character) : base(baseAttribute, vitalityCoef, strengthCoef, agilityCoef, intelligenceCoef, perLevel, character)
    {
    }

    public AttackSpeedAttribute(InitialAttribute attribute, CharacterBase character) : base(attribute, character)
    {
    }
}


[Serializable]
public struct InitialAttribute
{
    public float startingValue;
    public float strengthCoef;
    public float vitalityCoef;
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
}