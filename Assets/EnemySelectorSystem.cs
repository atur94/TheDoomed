using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectorSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameCharacterType[] allEnemiesTypes;

    public Dictionary<int, Enemy> allEnemies = new Dictionary<int, Enemy>();

    private void Awake()
    {
        for (int i = 0; i < allEnemiesTypes.Length; i++)
        {
            GameCharacterType newEnemyType = allEnemiesTypes[i];
            Enemy newEnemy = new Enemy(i,
                newEnemyType.characterName,
                newEnemyType.characterPrefab,
                newEnemyType.maxHealth,
                newEnemyType.maxHealth,
                newEnemyType.mana,
                newEnemyType.healthRegenPerRound,
                newEnemyType.manaRegenPerRound,
                newEnemyType.attackDamage,
                newEnemyType.attackSpeed,
                newEnemyType.armor,
                newEnemyType.magicReduction,
                newEnemyType.projectileDefault, 
                0f);
            allEnemies[i] = newEnemy;
            print($"Enemy {newEnemy.characterName} added to dictionary");
        }
    }
}
