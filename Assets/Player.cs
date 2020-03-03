using UnityEngine;

public class Player : GameCharacter
{
    public Player(int enemyId, GameCharacterType gameCharacterType) : base(enemyId, gameCharacterType) { }

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