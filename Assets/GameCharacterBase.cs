using System;
using UnityEngine;

public class GameCharacterBase : MonoBehaviour
{
    public int enemyId;
    public string characterName;
    public GameObject characterPrefab;

    public float currentHealth;
    public float currentMana;
    public AttackType attackType;

    [Header("Base current mana and health")]
    public float currentHealthRegenBase;
    public float currentManaRegenBase;

    [Header("Base character statistics")]
    public float maxHealthBase;
    public float maxManaBase;
    public float attackDamageBase;
    public float attackSpeedBase;
    public float attackRangeBase;
    public float movementSpeedBase;
    public float armorBase;
    public float magicReductionBase;

    [Header("Base per level stats")]
    public float healthPerLevelBase;
    public int manaPerLevelBase;
    public int attackDamagePerLevelBase;
    public float attackSpeedPerLevelBase;
    public float healthRegenPerLevelBase;
    public float manaRegenPerLevelBase;
    public float armorPerLevelBase;
    public float magicReductionPerLevelBase;

    [Header("Calculated mana and health")]
    public float currentHealthRegen;
    public float currentManaRegen;

    [Header("Calculated character statistics")]
    public float maxHealth;
    public float maxMana;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float movementSpeed;
    public float armor;
    public float magicReduction;

    [Header("Calculated per level stats")]
    public float healthPerLevel;
    public int manaPerLevel;
    public int attackDamagePerLevel;
    public float attackSpeedPerLevel;
    public float healthRegenPerLevel;
    public float manaRegenPerLevel;
    public float armorPerLevel;
    public float magicReductionPerLevel;

    [Header("Flat mana and health")]
    public float currentHealthRegenFlat;
    public float currentManaRegenFlat;

    [Header("Flat character statistics")]
    public float maxHealthFlat;
    public float maxManaFlat;
    public float attackDamageFlat;
    public float attackSpeedFlat;
    public float attackRangeFlat;
    public float movementSpeedFlat;
    public float armorFlat;
    public float magicReductionFlat;

    [Header("Flat per level stats")]
    public float healthPerLevelFlat;
    public int manaPerLevelFlat;
    public int attackDamagePerLevelFlat;
    public float attackSpeedPerLevelFlat;
    public float healthRegenPerLevelFlat;
    public float manaRegenPerLevelFlat;
    public float armorPerLevelFlat;
    public float magicReductionPerLevelFlat;

    [Header("Percent character statistics")]
    public float maxHealthPercent;
    public float maxManaPercent;
    public float attackDamagePercent;
    public float attackSpeedPercent;
    public float attackRangePercent;
    public float movementSpeedPercent;
    public float armorPercent;
    public float magicReductionPercent;

    [Header("Percent per level stats")]
    public float healthPerLevelPercent;
    public int manaPerLevelPercent;
    public int attackDamagePerLevelPercent;
    public float attackSpeedPerLevelPercent;
    public float healthRegenPerLevelPercent;
    public float manaRegenPerLevelPercent;
    public float armorPerLevelPercent;
    public float magicReductionPerLevelPercent;

    [Header("Levels")]
    public int level;
    public int currentExperience;
    protected int[] ExperienceForLevel;

    [Header("Textures and models")]
    public GameObject projectileDefault;

    [SerializeField] protected float timeBetweenAttacks;

    public GameCharacterBase(int enemyId, GameCharacterType gameCharacterType)
    {
        this.enemyId = enemyId;
        this.characterName = gameCharacterType.characterName;
        this.characterPrefab = gameCharacterType.characterPrefab;

        currentHealth = gameCharacterType.maxHealthBase;
        currentHealthRegenBase = gameCharacterType.currentHealthRegenBase;
        currentMana = gameCharacterType.maxManaBase;
        currentManaRegenBase = gameCharacterType.currentManaRegenBase;

        maxHealthBase = gameCharacterType.maxHealthBase;
        maxManaBase = gameCharacterType.maxManaBase;
        attackDamageBase = gameCharacterType.attackDamageBase;
        attackSpeedBase = gameCharacterType.attackSpeedBase;
        attackRangeBase = gameCharacterType.attackRangeBase;
        attackType = gameCharacterType.attackType;
        movementSpeedBase = gameCharacterType.movementSpeedBase;
        armorBase = gameCharacterType.armorBase;
        magicReductionBase = gameCharacterType.magicReductionBase;

        healthPerLevelBase = gameCharacterType.healthPerLevelBase;
        manaPerLevelBase = gameCharacterType.manaPerLevelBase;
        attackDamagePerLevelBase = gameCharacterType.attackDamagePerLevelBase;
        attackSpeedPerLevelBase = gameCharacterType.attackSpeedPerLevelBase;
        healthPerLevelBase = gameCharacterType.healthPerLevelBase;
        manaPerLevelBase = gameCharacterType.manaPerLevelBase;
        armorPerLevelBase = gameCharacterType.armorPerLevelBase;
        magicReductionPerLevelBase = gameCharacterType.magicReductionPerLevelBase;

        level = gameCharacterType.level;

        projectileDefault = gameCharacterType.projectileDefault;
    }
}

[Serializable]
public struct GameCharacterType
{
    [Header("Common")]
    public int enemyId;
    public string characterName;
    public GameObject characterPrefab;

    [Header("Current mana and health")]
    public float currentHealthBase;
    public float currentHealthRegenBase;
    public float currentManaBase;
    public float currentManaRegenBase;

    [Header("Character statistics")]
    public int maxHealthBase;
    public int maxManaBase;
    public int attackDamageBase;
    public float attackSpeedBase;
    public float attackRangeBase;
    public AttackType attackType;
    public float movementSpeedBase;
    public int armorBase;
    public int magicReductionBase;

    [Header("Per level stats")]
    public float healthPerLevelBase;
    public int manaPerLevelBase;
    public int attackDamagePerLevelBase;
    public float attackSpeedPerLevelBase;
    public float healthRegenPerLevelBase;
    public float manaRegenPerLevelBase;
    public float armorPerLevelBase;
    public float magicReductionPerLevelBase;

    [Header("Levels")]
    public int level;
    public int currentExperience;

    [Header("Textures and models")]
    public GameObject projectileDefault;

}

public enum AttackType
{
    Melee,
    Range
}