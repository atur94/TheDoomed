using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public abstract class Character : CharacterBase
{
    public abstract KeyValuePair<int, string> group { get; }

    public Disables disables;

    private StatusBar statusBar;

    public CharacterController characterController;
    private Controller _controller;
    public float[] experienceToLevelUp;

    public List<ItemSlot> itemSlots;
    public ItemSlot weaponSlot;

    public List<Status> statuses;
    private float _currentHealth;

    public void Start()
    {
        Initialize(this);
        statusBar = GetComponentInChildren<StatusBar>();
        statusBar.SetHealthPointsPercentage(1f);
        characterController = GetComponent<CharacterController>();
        _controller = GetComponent<Controller>();
        InitializeSlots();
        currentHealth = health.Value;
        currentMana = mana.Value;
    }



    private void InitializeSlots()
    {
        weaponSlot = ScriptableObject.CreateInstance<ItemSlot>();
        weaponSlot.character = this;
        weaponSlot.itemTypeRestriction = typeof(Weapon);
        weaponSlot.PlaceItem(ScriptableObject.CreateInstance<Weapon>());
        itemSlots.Add(weaponSlot);
    }

    public void Awake()
    {
        itemSlots = new List<ItemSlot>();
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
        RegenerationPerSecond();

    }

    private void Update()
    {
        characterController.Move(Time.fixedDeltaTime * 20f * Vector3.down);
        _controller?.Loop();
        Move(MovingDirection);
        //        MovingDirection = Vector3.zero;
        _lastPosition = transform.position;
    }

    private void LateUpdate()
    {
        statusBar.SetHealthPoints(currentHealth, health.Value);
        statusBar.SetManaPoints(currentMana, mana.Value);
    }

    public Vector3 MovingDirection
    {
        get => _movingDirection;
        set => _movingDirection = value;
    }

    private Vector3 _moveVector;
    private Vector3 _lastPosition;
    private Vector3 _lookVector;
    private void Move(Vector3 motion)
    {
        float timeRes = Time.deltaTime;
        if (characterController != null)
        { 
            if (!disables.isStunned)
            {
                Vector3 moveDirection = timeRes * movementSpeed.Value * 0.06f * motion;
                _moveVector = Vector3.Lerp(_moveVector, moveDirection, 0.03f * (1+turnRate.Value));
                _moveVector.y = - 20f * timeRes;
                characterController.Move(_moveVector);
                if (characterController.velocity.magnitude > 0.1f)
                {
                    var position = transform.position;
                    var diff = position - _lastPosition;
                    TurnInDirection(diff);
                }
            }
        }
    }



    private float TurnInDirection(Vector3 desiredDirection)
    {
        _lookVector = Vector3.Lerp(_lookVector, Vector3.ProjectOnPlane(transform.position + desiredDirection, Vector3.up), 0.2f * (1 + turnRate.Value));
        _lookVector.y = transform.position.y;
        transform.LookAt(_lookVector);
//        Debug.Log($"_lookVector = {transform.position - _lookVector} ,dd = {desiredDirection}");

        return Vector3.SignedAngle(transform.position - _lookVector, desiredDirection, Vector3.up);
    }

    public bool IsMoving
    {
        get { return characterController.velocity.magnitude > 0.2f; }
    }

    public void Attack(Transform enemy)
    {
        MovingDirection = Vector3.zero;

        if (!IsMoving)
        {
            if (enemy == null) return;
            float angle = Mathf.Abs(TurnInDirection(enemy.position - transform.position));

            if (178f < angle && angle < 182f)
            {
                Debug.Log("Attack");
            }

        }

    }


    private protected Character() { }



    private float _regenerationCounter;
    private Vector3 _movingDirection;

    private void RegenerationPerSecond()
    {
        //Invoke in FixedUpdate
        if(_regenerationCounter > 1f)
        {
            if (currentHealth <= health.Value && currentHealth > 0)
            {
                if ((currentHealth + healthRegen.Value) < health.Value)
                {
                    currentHealth += healthRegen.Value;
                }
                else
                {
                    currentHealth = health.Value;
                }
            }

            if (currentMana <= mana.Value && currentMana > 0)
            {
                if (currentMana + manaRegen.Value < mana.Value)
                {
                    currentMana += manaRegen.Value;
                }
                else
                {
                    currentMana = mana.Value;
                }
            }

            _regenerationCounter = 0;
        }

        _regenerationCounter += Time.fixedDeltaTime;
    }

}


