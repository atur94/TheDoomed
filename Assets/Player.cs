using UnityEngine;

public class Player : GameCharacter
{
    public Player(int enemyId, GameCharacterType gameCharacterType) : base(enemyId, gameCharacterType) { }

    public void Awake()
    {
        maxHealthBase = 100;
        attackSpeedBase = 1f;
        attackDamageBase = 25;
        currentHealthBase = 100;
        maxManaBase = 30;
        movementSpeedBase = 300f;
    }
}