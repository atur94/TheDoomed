using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public abstract class Base : ScriptableObject, IItemAttributes
{
    public int id;
    public string name;
    public abstract void Initialize();

    private AttributeSet _vitality;
    private AttributeSet _strength;
    private AttributeSet _agility;
    private AttributeSet _intelligence;
    private AttributeSet _physicalAttack;
    private AttributeSet _magicPower;
    private AttributeSet _health;
    private AttributeSet _mana;
    private AttributeSet _physicalDefense;
    private AttributeSet _magicalDefense;
    private AttributeSet _criticalChance;
    private AttributeSet _criticalDamage;
    private AttributeSet _evasion;
    private AttributeSet _accuracy;
    private AttributeSet _movementSpeed;
    private AttributeSet _healthRegen;
    private AttributeSet _manaRegen;
    private AttributeSet _fieldOfViewAngle;
    private AttributeSet _fieldOfViewRangeLight;
    private AttributeSet _fieldOfViewRangeDark;
    private AttributeSet _turnRate;
    private AttributeSet _channelingTimeReduction;
    private AttributeSet _castingTimeReduction;
    private AttributeSet _cooldownTimeReduction;
    private AttributeSet _attackSpeed;
    private AttributeSet _attackRange;

    public List<AttributeSet> StatsEffects = new List<AttributeSet>();

    public void AddToList(AttributeSet attribute)
    {
        if (StatsEffects.Contains(attribute)) return;

        StatsEffects.Add(attribute);
    }

    public AttributeSet Vitality
    {
        get { return _vitality; }
        set
        { _vitality = value; AddToList(value);}
    }

    public AttributeSet Strength
    {
        get { return _strength; }
        set { _strength = value; AddToList(value);}
    }

    public AttributeSet Agility
    {
        get { return _agility; }
        set { _agility = value; AddToList(value);}
    }

    public AttributeSet Intelligence
    {
        get { return _intelligence; }
        set { _intelligence = value; AddToList(value);}
    }

    public AttributeSet PhysicalAttack
    {
        get { return _physicalAttack; }
        set { _physicalAttack = value; AddToList(value);}
    }

    public AttributeSet MagicPower
    {
        get { return _magicPower; }
        set { _magicPower = value; AddToList(value);}
    }

    public AttributeSet Health
    {
        get { return _health; }
        set { _health = value; AddToList(value);}
    }

    public AttributeSet Mana
    {
        get { return _mana; }
        set { _mana = value; AddToList(value);}
    }

    public AttributeSet PhysicalDefense
    {
        get { return _physicalDefense; }
        set { _physicalDefense = value; AddToList(value);}
    }

    public AttributeSet MagicalDefense
    {
        get { return _magicalDefense; }
        set { _magicalDefense = value; AddToList(value);}
    }

    public AttributeSet CriticalChance
    {
        get { return _criticalChance; }
        set { _criticalChance = value; AddToList(value);}
    }

    public AttributeSet CriticalDamage
    {
        get { return _criticalDamage; }
        set { _criticalDamage = value; AddToList(value);}
    }

    public AttributeSet Evasion
    {
        get { return _evasion; }
        set { _evasion = value; AddToList(value);}
    }

    public AttributeSet Accuracy
    {
        get { return _accuracy; }
        set { _accuracy = value; AddToList(value);}
    }

    public AttributeSet MovementSpeed
    {
        get { return _movementSpeed; }
        set { _movementSpeed = value; AddToList(value);}
    }

    public AttributeSet HealthRegen
    {
        get { return _healthRegen; }
        set { _healthRegen = value; AddToList(value);}
    }

    public AttributeSet ManaRegen
    {
        get { return _manaRegen; }
        set { _manaRegen = value; AddToList(value);}
    }

    public AttributeSet FieldOfViewAngle
    {
        get { return _fieldOfViewAngle; }
        set { _fieldOfViewAngle = value; AddToList(value);}
    }

    public AttributeSet FieldOfViewRangeLight
    {
        get { return _fieldOfViewRangeLight; }
        set { _fieldOfViewRangeLight = value; AddToList(value);}
    }

    public AttributeSet FieldOfViewRangeDark
    {
        get { return _fieldOfViewRangeDark; }
        set { _fieldOfViewRangeDark = value; AddToList(value);}
    }

    public AttributeSet TurnRate
    {
        get { return _turnRate; }
        set { _turnRate = value; AddToList(value);}
    }

    public AttributeSet ChannelingTimeReduction
    {
        get { return _channelingTimeReduction; }
        set { _channelingTimeReduction = value; AddToList(value);}
    }

    public AttributeSet CastingTimeReduction
    {
        get { return _castingTimeReduction; }
        set { _castingTimeReduction = value; AddToList(value);}
    }

    public AttributeSet CooldownTimeReduction
    {
        get { return _cooldownTimeReduction; }
        set { _cooldownTimeReduction = value; AddToList(value);}
    }

    public AttributeSet AttackSpeed
    {
        get { return _attackSpeed; }
        set { _attackSpeed = value; AddToList(value);}
    }

    public AttributeSet AttackRange
    {
        get { return _attackRange; }
        set { _attackRange = value; AddToList(value);}
    }

}

[Serializable]
public class AttributeSet
{
    public AttributeSet(string name, AttributeType attributeType,float flatBonus, float percentBonus)
    {
        AttributeType = attributeType;
        FlatBonus = flatBonus;
        PercentBonus = percentBonus;
        Name = name;
    }

    public string FlatBonusToString => $"{Name}: {FlatBonus}";
    public string PercentBonusToString => $"{Name}: +{(int)(PercentBonus * 100)}%";

    public AttributeType AttributeType;
    public float FlatBonus;
    public float PercentBonus;
    public string Name;
}

public struct ItemAttributeInit
{
    public float FlatBonus { get; set; }
    public float Percent { get; set; }
}

public struct ItemStruct
{
    public ItemAttributeInit Vitality;
    public ItemAttributeInit Strength;
    public ItemAttributeInit Agility;
    public ItemAttributeInit Intelligence;
    public ItemAttributeInit PhysicalAttack;
    public ItemAttributeInit MagicPower;
    public ItemAttributeInit Health;
    public ItemAttributeInit Mana;
    public ItemAttributeInit PhysicalDefense;
    public ItemAttributeInit MagicalDefense;
    public ItemAttributeInit CriticalChance;
    public ItemAttributeInit CriticalDamage;
    public ItemAttributeInit Evasion;
    public ItemAttributeInit Accuracy;
    public ItemAttributeInit MovementSpeed;
    public ItemAttributeInit HealthRegen;
    public ItemAttributeInit ManaRegen;
    public ItemAttributeInit FieldOfViewAngle;
    public ItemAttributeInit FieldOfViewRangeLight;
    public ItemAttributeInit FieldOfViewRangeDark;
    public ItemAttributeInit TurnRate;
    public ItemAttributeInit ChannelingTimeReduction;
    public ItemAttributeInit CastingTimeReduction;
    public ItemAttributeInit CooldownTimeReduction;
    public ItemAttributeInit AttackSpeed;
}

public enum ItemType
{
    Weapon,
    Head,
    Chest,
    Wrists,
    Legs,
    Boots,
    Necklace,
    Ring,
    Orb,
}

interface IItemAttributes
{
     AttributeSet Vitality { get; set; }
     AttributeSet Strength { get; set; }
     AttributeSet Agility { get; set; }
     AttributeSet Intelligence { get; set; }
     AttributeSet PhysicalAttack { get; set; }
     AttributeSet MagicPower { get; set; }
     AttributeSet Health { get; set; }
     AttributeSet Mana { get; set; }
     AttributeSet PhysicalDefense { get; set; }
     AttributeSet MagicalDefense { get; set; }
     AttributeSet CriticalChance { get; set; }
     AttributeSet CriticalDamage { get; set; }
     AttributeSet Evasion { get; set; }
     AttributeSet Accuracy { get; set; }
     AttributeSet MovementSpeed { get; set; }
     AttributeSet HealthRegen { get; set; }
     AttributeSet ManaRegen { get; set; }
     AttributeSet FieldOfViewAngle { get; set; }
     AttributeSet FieldOfViewRangeLight { get; set; }
     AttributeSet FieldOfViewRangeDark { get; set; }
     AttributeSet TurnRate { get; set; }
     AttributeSet ChannelingTimeReduction { get; set; }
     AttributeSet CastingTimeReduction { get; set; }
     AttributeSet CooldownTimeReduction { get; set; }
     AttributeSet AttackSpeed { get; set; }
}