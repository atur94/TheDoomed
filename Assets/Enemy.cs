using UnityEngine;

public class Enemy : GameCharacter
{
    public Enemy(int enemyId, GameCharacterType gameCharacterType) : base(enemyId, gameCharacterType) { }


    public void Awake()
    {
        characterName = gameObject.name;
        maxHealthBase = 100;
        currentHealthBase = 100;
        attackDamageBase = 10;
        characterPrefab = null;
        attackSpeedBase = 0.5f;
    }
}