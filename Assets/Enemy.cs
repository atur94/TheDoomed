using UnityEngine;

public class Enemy : GameCharacter
{
    public Enemy(int enemyId, GameCharacterType gameCharacterType) : base(enemyId, gameCharacterType) { }


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