using System;
using Boo.Lang;
using UnityEngine;


public class GameCharacter : GameCharacterBase
{
    private StatusBar statusBar;

    public bool isStunned;
    
    private CharacterController characterController;
    
    [SerializeField]
    private Vector3 lookVector;

    private List<Disable> Disables;

    public void Start()
    {
        statusBar = GetComponentInChildren<StatusBar>();
        statusBar.SetHealthPointsPercentage(1f);
        characterController = GetComponent<CharacterController>();
        Disables = new List<Disable>();

    }

    public void Attack(Transform enemy)
    {
        if (enemy == null) return;

        transform.LookAt(enemy);
        if (Math.Abs(timeBetweenAttacks) > 0.01f) return;

        ProjectileType projectileType = default;
        projectileType.damage = attackDamage;
        projectileType.speed = 30f;
        projectileType.direction = (enemy.position - transform.position).normalized;


        var projectileObject = Instantiate(projectileDefault, transform.position, Quaternion.identity);
        Projectile.CreateProjectile(projectileObject, projectileType);
        timeBetweenAttacks = 1 / attackSpeed;
    }

    public void TakeDamage(int damage)
    {
        double damageMultiplication = 1 - (0.052 * armor) / (0.9 + 0.048 * Mathf.Abs(armor));
        var calculatedDamage = damage * damageMultiplication;
        if (currentHealth > calculatedDamage)
        {
            currentHealth -= (int)calculatedDamage;
            statusBar.SetHealthPointsPercentage(currentHealth/(float)maxHealth);
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
        DisableAllStates();
        ApplyEffects();
        if (timeBetweenAttacks > Time.fixedDeltaTime)
        {

            timeBetweenAttacks -= Time.fixedDeltaTime;
        }
        else
        {
            timeBetweenAttacks = 0;
        }

        
    }

    private void DisableAllStates()
    {
        isStunned = false;
    }

    private void ApplyEffects()
    {
        List<Disable> disablesToRemove = new List<Disable>();
        for (int i = 0; i < Disables.Count; i++)
        {
            Disable disable = Disables[i];
            if (disable.CanBeRemoved) disablesToRemove.Add(disable);
            else disable.Update(Time.fixedDeltaTime);
        }

        for (int i = 0; i < disablesToRemove.Count; i++)
        {
            Disables.Remove(disablesToRemove[i]);
        }
        
    }

    public void ApplyDisable(Disable appliedDisable)
    {
        if (appliedDisable.Stackable)
        {
            Disables.Add(appliedDisable);
            return;
        }

        for (var i = 0; i < Disables.Count; i++)
        {
            var disable = Disables[i];
            if (disable.Id == appliedDisable.Id)
            {
                disable.TimeLeft = appliedDisable.TimeLeft;
                return;
            }
        }

        Disables.Add(appliedDisable);

    }

    public void RemoveDisableFromDisables(Disable removeDisable)
    {
        
    }

    public virtual void Move(Vector3 motion, float timeRes)
    {
        if(characterController != null)
        {
            if(!isStunned)
            {
                characterController.Move(motion * timeRes);
                if (characterController.velocity.magnitude > 1f)
                {
                    var position = transform.position;
                    lookVector = Vector3.Lerp(lookVector, Vector3.ProjectOnPlane(position + motion, Vector3.up), 0.2f);
                    lookVector.y = position.y;
                    transform.LookAt(lookVector);
                }
            }
        }
    }

    public GameCharacter(int enemyId, GameCharacterType gameCharacterType) : base(enemyId, gameCharacterType) { }
}

