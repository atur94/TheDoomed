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
    public bool isMainAttribute = true;
    public AttributeSet(AttributeType attributeType,float flatBonus, float percentBonus)
    {
        this.attributeType = attributeType;
        FlatBonus = flatBonus;
        PercentBonus = percentBonus;
        _name = AttributeTypeToName(attributeType);
        SelectUnit();
    }

    void SelectUnit()
    {
        switch (attributeType)
        {
            case AttributeType.AttackSpeed:
                unit = "atk/s";
                break;
            default:
                unit = "";
                break;
        }
    }

    public string unit;

    public string FlatBonusFormat
    {
        get
        {
            if (!isMainAttribute) _flatBonusFormat = "{0:+0.##;-0.##;#} {1}";
            else _flatBonusFormat = "{0:0.##;-0.##;#} {1}";
            return _flatBonusFormat;
        }
        set => _flatBonusFormat = value;
    }

    public string PercentBonusFormat
    {
        get
        {
            _percentBonusFormat = "{0:+0%;-0%;0}";
            return _percentBonusFormat;
        }
        set => _percentBonusFormat = value;
    }

    public string FlatBonusAsString
    {
        get
        {
            return string.Format(FlatBonusFormat, FlatBonus, unit);
        }
    }

    public string PecentBonusAsString => string.Format(PercentBonusFormat, (PercentBonus));
    public AttributeType attributeType;
    public float FlatBonus;
    public float PercentBonus;

    public string Name
    {
        get
        {
            if (_name.Equals(""))
            {
                return AttributeTypeToName(attributeType);
            }

            return _name;
        }
        set { _name = value; }
    }

    public int order;
    private string _name;
    private string _flatBonusFormat;
    private string _percentBonusFormat;

    public static string AttributeTypeToName(AttributeType attributeType)
    {
        switch (attributeType)
        {
            case AttributeType.Vitality:
                return "Vitality";
            case AttributeType.Strength:
                return "Strength";
            case AttributeType.Agility:
                return "Agility";
            case AttributeType.Intelligence:
                return "Intelligence";
            case AttributeType.PhysicalAttack:
                return "Physical attack";
            case AttributeType.MagicPower: return "Magic power";
            case AttributeType.Health: return "Health";
            case AttributeType.Mana: return "Mana";
            case AttributeType.PhysicalDefense: return "Physical defense";
            case AttributeType.MagicalDefense: return "Magical defense";
            case AttributeType.CriticalChance: return "Critical chance";
            case AttributeType.CriticalDamage: return "Critical damage";
            case AttributeType.Evasion: return "Evasion";
            case AttributeType.Accuracy: return "Accuracy";
            case AttributeType.MovementSpeed: return "Movement speed";
            case AttributeType.HealthRegen: return "Health regeneration";
            case AttributeType.ManaRegen: return "Mana regeneration";
            case AttributeType.FieldOfViewAngle: return "Field of view(angle)";
            case AttributeType.FieldOfViewRangeLight: return "Field of view range(light)";
            case AttributeType.FieldOfViewRangeDark: return "Field of view range(dark)";
            case AttributeType.TurnRate: return "Turn rate";
            case AttributeType.ChannelingTimeReduction: return "Channeling time";
            case AttributeType.CastingTimeReduction: return "Casting time";
            case AttributeType.CooldownTimeReduction: return "Cooldown time";
            case AttributeType.AttackSpeed: return "NormalAttack speed";
            case AttributeType.AttackWeaponRange: return "NormalAttack range";
            case AttributeType.StorageCapacity: return "Storage capacity";
        }

        return "";
    }
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
    None,
    Weapon,
    Head,
    Chest,
    Wrists,
    Legs,
    Boots,
    Necklace,
    Ring,
    Orb,
    Backpack
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