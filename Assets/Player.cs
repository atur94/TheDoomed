using UnityEngine;

public class Player : GameCharacter
{
    public Player(int enemyId, string characterName, GameObject characterPrefab, int maxHealth, int currentHealth, int mana, int healthRegenPerRound, int manaRegenPerRound, int attackDamage, float attackSpeed, int armor, int magicReduction, GameObject projectileDefault, float timeBetweenAttacks) : base(enemyId, characterName, characterPrefab, maxHealth, currentHealth, mana, healthRegenPerRound, manaRegenPerRound, attackDamage, attackSpeed, armor, magicReduction, projectileDefault, timeBetweenAttacks)
    {
    }

    public void Awake()
    {
        maxHealth = 100;
        attackSpeed = 1f;
        attackDamage = 25;
        currentHealth = 100;
        healthRegenPerRound = 30;
        mana = 30;
    }
}