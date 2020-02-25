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
    public int healthRegenPerRound;
    public int manaRegenPerRound;
    public int attackDamage;
    public float attackSpeed;
    public int armor;
    public int magicReduction;
    public GameObject projectileDefault;

    [SerializeField] protected float timeBetweenAttacks;

    public GameCharacterBase(int enemyId, string characterName, GameObject characterPrefab, int maxHealth, int currentHealth, int mana, int healthRegenPerRound, int manaRegenPerRound, int attackDamage, float attackSpeed, int armor, int magicReduction, GameObject projectileDefault, float timeBetweenAttacks)
    {
        this.enemyId = enemyId;
        this.characterName = characterName;
        this.characterPrefab = characterPrefab;
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
        this.mana = mana;
        this.healthRegenPerRound = healthRegenPerRound;
        this.manaRegenPerRound = manaRegenPerRound;
        this.attackDamage = attackDamage;
        this.attackSpeed = attackSpeed;
        this.armor = armor;
        this.magicReduction = magicReduction;
        this.projectileDefault = projectileDefault;
        this.timeBetweenAttacks = timeBetweenAttacks;
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
    public int healthRegenPerRound;
    public int manaRegenPerRound;
    public int attackDamage;
    public float attackSpeed;
    public int armor;
    public int magicReduction;
    public GameObject projectileDefault;
    public int enemyId;
}