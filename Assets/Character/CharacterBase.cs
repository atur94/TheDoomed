using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public abstract partial class CharacterBase : MonoBehaviour, INotifyPropertyChanged
{
    public int level;
    private int? _id;

    private static int _idCounter;
    private static int IdCounter
    {
        get
        {
            _idCounter++;
            return _idCounter;
        }
    }

    public int Id
    {
        get
        {
            if (_id == null)
            {
                _id = IdCounter;
            }

            return _id.Value;
        }
        set => _id = value;
    }

    protected int MaxLevel = 25;
    public int PointsForDistribution
    {
        get
        {
            if (level < 2) return 0;
            return ((level - 1) * 5) - _pointsDistributed;
            
        }
    }

    private int _pointsDistributed;


    public Attributes BaseAttributes;


    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value; 
            OnPropertyChanged();
        }
    }

    public float CurrentMana
    {
        get => _currentMana;
        set
        {
            _currentMana = value;
            OnPropertyChanged();
        }
    }

    protected Rigidbody rb;
    private float _currentHealth;
    private float _currentMana;

    protected void Initialize(Character character)
    {
        _pointsDistributed = 0;
        rb = GetComponent<Rigidbody>();
        Id = IdCounter;
        InitializeList(character);
    }

    public int AddStatPoint(AttributeType attributeType)
    {
        if (PointsForDistribution <= 0) return 0;


        switch (attributeType)
        {
            case AttributeType.Vitality:
                vitality.Added++;
                break;
            case AttributeType.Strength:
                strength.Added++;
                break;
            case AttributeType.Agility:
                agility.Added++;
                break;
            case AttributeType.Intelligence:
                intelligence.Added++;
                break;
        }

        return _pointsDistributed++;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {

    }
}

public class CommonAttribute : INotifyPropertyChanged
{
    public static List<CommonAttribute> initList;

    public bool IsChanged
    {
        get => _isChanged;
        set
        {
            _isChanged = value;
            OnPropertyChanged();
        }
    }

    protected Character Character;
    protected float _base;  
    public virtual float Base
    {
        protected get =>
            _base + StrengthCoef * Character.strength.Value + VitalityCoef * Character.vitality.Value +
            AgilityCoef * Character.agility.Value + IntelligenceCoef * Character.intelligence.Value + PerLevel * Character.level;
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

    public float FlatBonuses
    {
        get
        {
            float sum = 0;
            for(int i = 0; i < Character.itemSlots.Count; i++){
                Base item = Character.itemSlots[i].ItemInSlot;
                if (item != null)
                {
                    for (int j = 0; j < item.StatsEffects.Count; j++)
                    {
                        AttributeSet attribute = item.StatsEffects[j];
                        if (attribute.attributeType == AttributeType)
                        {

                                sum += attribute.FlatBonus;
                        }
                    }
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
                Base item = Character.itemSlots[i].ItemInSlot;
                if (item != null)
                {
                    for (int j = 0; j < item.StatsEffects.Count; j++)
                    {
                        AttributeSet attribute = item.StatsEffects[j];
                        if (attribute.attributeType == AttributeType)
                        {
                            sum += attribute.PercentBonus;
                        }
                    };

                }
            }
            return sum;
        }
    }

    private float _value = 0f;
    private bool _isChanged = true;

    public virtual float Value
    {
        get
        {
            return _value;
        }
    }

 


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
        if (initList != null)
        {
            initList.Add(this);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        _value = (Base + FlatBonuses) * (1 + PercentBonuses);
        _isChanged = false;
    }
}

public class MainAttribute : CommonAttribute
{
    
    public float Added;

    public override float Base
    {
        protected get => _base + Added + PerLevel * Character.level;
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
                            PerLevel * Character.level);
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