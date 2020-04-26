using System.Collections.Generic;
using UnityEngine;

partial class CharacterBase
{
    public MainAttribute vitality;
    public MainAttribute strength;
    public MainAttribute agility;
    public MainAttribute intelligence;
    public CommonAttribute physicalAttack;
    public CommonAttribute magicPower;
    public CommonAttribute health;
    public CommonAttribute mana;
    public CommonAttribute physicalDefense;
    public CommonAttribute magicalDefense;
    public CommonAttribute criticalChance;
    public CommonAttribute criticalDamage;
    public CommonAttribute evasion;
    public CommonAttribute accuracy;
    public CommonAttribute movementSpeed;
    public CommonAttribute healthRegen;
    public CommonAttribute manaRegen;
    public CommonAttribute fieldOfViewAngle;
    public CommonAttribute fieldOfViewRangeLight;
    public CommonAttribute fieldOfViewRangeDark;
    public CommonAttribute turnRate;
    public TimeAttribute channelingTimeReduction;
    public TimeAttribute castingTimeReduction;
    public TimeAttribute cooldownTimeReduction;
    public CommonAttribute attackSpeed;
    public CommonAttribute attackWeaponRange;

    public List<CommonAttribute> attributes; 
    private void InitializeList(Character character)
    {
        attributes = new List<CommonAttribute>();
        CommonAttribute.initList = attributes;
        vitality = new MainAttribute(BaseAttributes.vitality, character, AttributeType.Vitality);
        strength = new MainAttribute(BaseAttributes.strength, character, AttributeType.Strength);
        agility = new MainAttribute(BaseAttributes.agility, character, AttributeType.Agility);
        intelligence = new MainAttribute(BaseAttributes.intelligence, character, AttributeType.Intelligence);

        physicalAttack = new CommonAttribute(BaseAttributes.physicalAttack, character, AttributeType.PhysicalAttack);
        magicPower = new CommonAttribute(BaseAttributes.magicPower, character, AttributeType.MagicPower);

        health = new CommonAttribute(BaseAttributes.health, character, AttributeType.Health);
        mana = new CommonAttribute(BaseAttributes.mana, character, AttributeType.Mana);

        physicalDefense = new CommonAttribute(BaseAttributes.physicalDefense, character, AttributeType.PhysicalDefense);
        magicalDefense = new CommonAttribute(BaseAttributes.magicalDefense, character, AttributeType.MagicalDefense);

        criticalChance = new CommonAttribute(BaseAttributes.criticalChance, character, AttributeType.CriticalChance);
        criticalDamage = new CommonAttribute(BaseAttributes.criticalDamage, character, AttributeType.CriticalDamage);

        evasion = new CommonAttribute(BaseAttributes.evasion, character, AttributeType.Evasion);
        accuracy = new CommonAttribute(BaseAttributes.accuracy, character, AttributeType.Accuracy);
        movementSpeed = new CommonAttribute(BaseAttributes.movementSpeed, character, AttributeType.MovementSpeed);

        healthRegen = new CommonAttribute(BaseAttributes.healthRegen, character, AttributeType.HealthRegen);
        manaRegen = new CommonAttribute(BaseAttributes.manaRegen, character, AttributeType.ManaRegen);

        fieldOfViewAngle = new CommonAttribute(BaseAttributes.fieldOfViewAngle, character, AttributeType.FieldOfViewAngle);
        fieldOfViewRangeLight = new CommonAttribute(BaseAttributes.fieldOfViewRangeLight, character, AttributeType.FieldOfViewRangeLight);
        fieldOfViewRangeDark = new CommonAttribute(BaseAttributes.fieldOfViewRangeDark, character, AttributeType.FieldOfViewRangeDark);
        turnRate = new CommonAttribute(BaseAttributes.turnRate, character, AttributeType.TurnRate);

        channelingTimeReduction = new TimeAttribute(BaseAttributes.channelingTime, character, AttributeType.ChannelingTimeReduction);
        castingTimeReduction = new TimeAttribute(BaseAttributes.castingTime, character, AttributeType.CastingTimeReduction);
        cooldownTimeReduction = new TimeAttribute(BaseAttributes.cooldownReduction, character, AttributeType.CooldownTimeReduction);

        attackSpeed = new CommonAttribute(BaseAttributes.attackSpeed, character, AttributeType.AttackSpeed);
        CommonAttribute.initList = null;
    }
}

public enum AttributeType
{
  Vitality,
  Strength,
  Agility,
  Intelligence,
  PhysicalAttack,
  MagicPower,
  Health,
  Mana,
  PhysicalDefense,
  MagicalDefense,
  CriticalChance,
  CriticalDamage,
  Evasion,
  Accuracy,
  MovementSpeed,
  HealthRegen,
  ManaRegen,
  FieldOfViewAngle,
  FieldOfViewRangeLight,
  FieldOfViewRangeDark,
  TurnRate,
  ChannelingTimeReduction,
  CastingTimeReduction,
  CooldownTimeReduction,
  AttackSpeed,
  AttackWeaponRange,
  StorageCapacity
}