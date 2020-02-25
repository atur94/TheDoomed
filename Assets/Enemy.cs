using UnityEngine;

public class Enemy : GameCharacter
{
    public Enemy(int enemyId, string characterName, GameObject characterPrefab, int maxHealth, int currentHealth, int mana, int healthRegenPerRound, int manaRegenPerRound, int attackDamage, float attackSpeed, int armor, int magicReduction, GameObject projectileDefault, float timeBetweenAttacks) : base(enemyId, characterName, characterPrefab, maxHealth, currentHealth, mana, healthRegenPerRound, manaRegenPerRound, attackDamage, attackSpeed, armor, magicReduction, projectileDefault, timeBetweenAttacks)
    {
    }

    public void Awake()
    {
        characterName = gameObject.name;
        maxHealth = 100;
        currentHealth = 100;
        attackDamage = 10;
        characterPrefab = null;
        attackSpeed = 0.5f;
    }
}