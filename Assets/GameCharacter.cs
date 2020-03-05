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

    public float movmentSpeedCalculation = 0f;
    private float CalculatedMovementSpeed
    {
        get
        {
            float speed = movementSpeedBase * (1 - movmentSpeedCalculation);
            return speed;
        }
    }

    [SerializeField]
    public List<Status> Statuses;


    public void Start()
    {
        statusBar = GetComponentInChildren<StatusBar>();
        statusBar.SetHealthPointsPercentage(1f);
        characterController = GetComponent<CharacterController>();
        Statuses = new List<Status>();

    }

    public void Attack(Transform enemy)
    {
        if (enemy == null) return;

        transform.LookAt(enemy);
        if (Math.Abs(timeBetweenAttacks) > 0.01f) return;

        ProjectileType projectileType = default;
        projectileType.damage = attackDamageBase;
        projectileType.speed = 30f;
        projectileType.direction = (enemy.position - transform.position).normalized;


        var projectileObject = Instantiate(projectileDefault, transform.position, Quaternion.identity);
        Projectile.CreateProjectile(projectileObject, projectileType);
        timeBetweenAttacks = 1 / attackSpeedBase;
    }

    public void TakeDamage(int damage)
    {
        double damageMultiplication = 1 - (0.052 * armorBase) / (0.9 + 0.048 * Mathf.Abs(armorBase));
        var calculatedDamage = damage * damageMultiplication;
        if (currentHealthBase > calculatedDamage)
        {
            currentHealthBase -= (int)calculatedDamage;
            statusBar.SetHealthPointsPercentage(currentHealthBase/(float)maxHealthBase);
        }
        else
        {
            currentHealthBase = 0;
            Debug.Log(currentHealthBase);

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
        movmentSpeedCalculation = 0f;
    }

    private void ApplyEffects()
    {
        List<Status> statusesToRemove = new List<Status>();
        for (int i = 0; i < Statuses.Count; i++)
        {
            Status status = Statuses[i];
            if (status.CanBeRemoved) statusesToRemove.Add(status);
            else status.Update(Time.fixedDeltaTime);
        }

        for (int i = 0; i < statusesToRemove.Count; i++)
        {
            Statuses.Remove(statusesToRemove[i]);
        }
        
    }

    public void ApplyDisable(Status appliedStatus)
    {
        if (appliedStatus.Stackable)
        {
            Statuses.Add(appliedStatus);
            return;
        }

        for (var i = 0; i < Statuses.Count; i++)
        {
            var status = Statuses[i];
            if (status.Id == appliedStatus.Id)
            {
                status.TimeLeft = appliedStatus.TimeLeft;
                return;
            }
        }

        Statuses.Add(appliedStatus);

    }

    public void RemoveStatusFromStatuses(Status removeStatus)
    {
        
    }

    public virtual void Move(Vector3 motion, float timeRes)
    {
        if(characterController != null)
        {
            if(!isStunned)
            {
                characterController.Move(timeRes * CalculatedMovementSpeed * 0.05f * motion);
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

