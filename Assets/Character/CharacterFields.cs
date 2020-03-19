
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

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

    public TimeAttribute channelingTimeReduction;
    public TimeAttribute castingTimeReduction;
    public TimeAttribute cooldownTimeReduction;

    public AttackSpeedAttribute attackSpeed;

    public List<CommonAttribute> attributesAll = new List<CommonAttribute>();

    private void InitializeList()
    {
        vitality = new MainAttribute(BaseAttributes.vitality, this);
        strength = new MainAttribute(BaseAttributes.strength, this);
        agility = new MainAttribute(BaseAttributes.agility, this);
        intelligence = new MainAttribute(BaseAttributes.intelligence, this);

        physicalAttack = new CommonAttribute(BaseAttributes.physicalAttack, this);
        magicPower = new CommonAttribute(BaseAttributes.magicPower, this);
        health = new CommonAttribute(BaseAttributes.health, this);
        mana = new CommonAttribute(BaseAttributes.mana, this);
        physicalDefense = new CommonAttribute(BaseAttributes.physicalDefense, this);
        magicalDefense = new CommonAttribute(BaseAttributes.magicalDefense, this);
        criticalChance = new CommonAttribute(BaseAttributes.criticalChance, this);
        criticalDamage = new CommonAttribute(BaseAttributes.criticalDamage, this);
        evasion = new CommonAttribute(BaseAttributes.evasion, this);
        accuracy = new CommonAttribute(BaseAttributes.accuracy, this);
        movementSpeed = new CommonAttribute(BaseAttributes.movementSpeed, this);
        healthRegen = new CommonAttribute(BaseAttributes.healthRegen, this);
        manaRegen = new CommonAttribute(BaseAttributes.manaRegen, this);
        fieldOfViewAngle = new CommonAttribute(BaseAttributes.fieldOfViewAngle, this);
        fieldOfViewRangeLight = new CommonAttribute(BaseAttributes.fieldOfViewRangeLight, this);
        fieldOfViewRangeDark = new CommonAttribute(BaseAttributes.fieldOfViewRangeDark, this);

        channelingTimeReduction = new TimeAttribute(BaseAttributes.channelingTime, this);
        castingTimeReduction = new TimeAttribute(BaseAttributes.castingTime, this);
        cooldownTimeReduction = new TimeAttribute(BaseAttributes.cooldownReduction, this);
    }
}
