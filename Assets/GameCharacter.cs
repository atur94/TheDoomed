using System;
using UnityEngine;


public class GameCharacter : GameCharacterBase
{
    private HealthBar healthBar;
    


    public void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetHealthPointsPercentage(1f);
    }

    public void Attack(Transform enemy)
    {
        if (enemy == null) return;

        transform.LookAt(enemy);
        if (Math.Abs(timeBetweenAttacks) > 0.01f) return;

        ProjectileType projectileType;
        projectileType.damage = attackDamage;
        projectileType.speed = 10f;

        
        var projectileObject = Instantiate(projectileDefault, transform.position, Quaternion.identity);
        var projectile = projectileObject.AddComponent<Projectile>();
        projectile.damage = attackDamage;
        projectile.speed = 30f;
        var direction = (enemy.position - transform.position).normalized;
        projectile.direction = direction;
        timeBetweenAttacks = 1 / attackSpeed;
    }

    public void TakeDamage(int damage)
    {
        double damageMultiplication = 1 - (0.052 * armor) / (0.9 + 0.048 * Mathf.Abs(armor));
        var calculatedDamage = damage * damageMultiplication;
        if (currentHealth > calculatedDamage)
        {
            currentHealth -= (int)calculatedDamage;
            healthBar.SetHealthPointsPercentage(currentHealth/(float)maxHealth);
        }
        else
        {
            currentHealth = 0;
            Debug.Log(currentHealth);

            Destroy(gameObject);
        }

    }

    void FixedUpdate()
    {
        if (timeBetweenAttacks > Time.fixedDeltaTime)
        {

            timeBetweenAttacks -= Time.fixedDeltaTime;
        }
        else
        {
            timeBetweenAttacks = 0;
        }
    }

    public GameCharacter(int enemyId, string characterName, GameObject characterPrefab, int maxHealth, int currentHealth, int mana, int healthRegenPerRound, int manaRegenPerRound, int attackDamage, float attackSpeed, int armor, int magicReduction, GameObject projectileDefault, float timeBetweenAttacks) : base(enemyId, characterName, characterPrefab, maxHealth, currentHealth, mana, healthRegenPerRound, manaRegenPerRound, attackDamage, attackSpeed, armor, magicReduction, projectileDefault, timeBetweenAttacks)
    {
    }
}

