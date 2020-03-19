using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : CharacterBase
{
    public float currentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value; 
            statusBar.SetHealthPointsPercentage(currentHealth/health.Value);
        }
    }

    public float currentMana;

    public Disables disables;

    private StatusBar statusBar;

    public CharacterController characterController;


    public float[] experienceToLevelUp;

    [SerializeField]
    private Vector3 lookVector;

    public List<Status> statuses;
    private float _currentHealth;

    public void Start()
    {
        Initialize();
        statusBar = GetComponentInChildren<StatusBar>();
        statusBar.SetHealthPointsPercentage(1f);
        characterController = GetComponent<CharacterController>();
        currentHealth = health.Value;
        currentMana = mana.Value;

    }

    public void Awake()
    {

        currentMana = mana.Value;
        experienceToLevelUp = new float[MaxLevel];

        for (var index = 0; index < experienceToLevelUp.Length; index++)
        {
            experienceToLevelUp[index] = 100f;
        }
    }

    public void DealDamage(Damage damage)
    {
        double physicalDamageReduction = 1 - (0.052 * physicalDefense.Value) / (0.9 + 0.048 * Mathf.Abs(physicalDefense.Value));
        double magicalDamageReduction = 1 - (0.052 * magicalDefense.Value) / (0.9 + 0.048 * Mathf.Abs(magicalDefense.Value));
        int calculatedDamage = (int)(physicalDamageReduction * damage.PhysicalDamage +
                                magicalDamageReduction * damage.MagicalDamage);

        if (currentHealth > calculatedDamage)
        {
            currentHealth -= calculatedDamage;
        }
        else
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
    }

    public void BurnMana(float manaToBurn)
    {
        if (currentMana > manaToBurn)
        {
            currentMana -= manaToBurn;
        }
    }

    private void FixedUpdate()
    {
        disables.Reset();
        statusBar.SetHealthPoints(currentHealth, health.Value);
        statusBar.SetManaPoints(currentMana, mana.Value);
    }

    public virtual void Move(Vector3 motion, float timeRes)
    {
        if (characterController != null)
        {
            if (!disables.isStunned)
            {
                characterController.Move(timeRes * movementSpeed.Value * 0.05f * motion);
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

    private protected Character() { }


}


