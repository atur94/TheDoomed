using System;
using UnityEngine;

public class GameCharacterBase : MonoBehaviour
{
    public int enemyId;
    public string characterName;
    public GameObject characterPrefab;
    public int maxHealth;
    public int currentHealth;
    public int mana;
    public int manaPerLevel;
    public int healthRegenPerRound;
    public int healthRegenPerRoundPerLevel;
    public int manaRegenPerRound;
    public int manaRegenPerRoundPerLevel;
    public int attackDamage;
    public int attackDamagePerLevel;
    public float attackSpeed;
    public float attackSpeedPerLevel;
    public int armor;
    public int armorPerLevel;
    public int magicReduction;
    public int magicReductionPerLevel;
    public int level;
    public int currentExperience;

    protected int[] ExperienceForLevel;

    public GameObject projectileDefault;

    [SerializeField] protected float timeBetweenAttacks;

    public GameCharacterBase(int enemyId, GameCharacterType gameCharacterType)
    {
        this.enemyId = enemyId;
        this.characterName = gameCharacterType.characterName;
        this.characterPrefab = gameCharacterType.characterPrefab;
        this.maxHealth = gameCharacterType.maxHealth;
        this.currentHealth = gameCharacterType.currentHealth;
        this.mana = gameCharacterType.mana;
        this.manaPerLevel = gameCharacterType.manaPerLevel;
        this.healthRegenPerRound = gameCharacterType.healthRegenPerRound;
        this.healthRegenPerRoundPerLevel = gameCharacterType.healthRegenPerRoundPerLevel;
        this.manaRegenPerRound = gameCharacterType.manaRegenPerRound;
        this.manaRegenPerRoundPerLevel = gameCharacterType.manaRegenPerRoundPerLevel;
        this.attackDamage = gameCharacterType.attackDamage;
        this.attackDamagePerLevel = gameCharacterType.attackDamagePerLevel;
        this.attackSpeed = gameCharacterType.attackSpeed;
        this.attackSpeedPerLevel = gameCharacterType.attackSpeedPerLevel;
        this.armor = gameCharacterType.armor;
        this.armorPerLevel = gameCharacterType.armorPerLevel;
        this.magicReduction = gameCharacterType.magicReduction;
        this.magicReductionPerLevel = gameCharacterType.magicReductionPerLevel;
        this.level = gameCharacterType.level;
        this.currentExperience = gameCharacterType.currentExperience;
        this.projectileDefault = gameCharacterType.projectileDefault;
    }
}

[Serializable]
public struct GameCharacterType
{
    public string characterName;
    public GameObject characterPrefab;
    public int maxHealth;
    public int currentHealth;
    public int mana;
    public int manaPerLevel;
    public int healthRegenPerRound;
    public int healthRegenPerRoundPerLevel;
    public int manaRegenPerRound;
    public int manaRegenPerRoundPerLevel;
    public int attackDamage;
    public int attackDamagePerLevel;
    public float attackSpeed;
    public float attackSpeedPerLevel;
    public int armor;
    public int armorPerLevel;
    public int magicReduction;
    public int magicReductionPerLevel;
    public int level;
    public int currentExperience;
    public GameObject projectileDefault;
}