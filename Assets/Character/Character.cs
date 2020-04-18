using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public interface IDamagable
{
    void DealDamage(Damage damage);
}

public abstract partial class Character : CharacterBase, IDamagable
{
    private Animator _animator;
    private WeaponSelector _weaponSelector;
    public abstract KeyValuePair<int, string> group { get; }

    public Disables disables;

    private StatusBar statusBar;

    public CharacterController characterController;
    private Controller _controller;
    public float[] experienceToLevelUp;

    public List<ItemSlot> itemSlots;

    public ItemSlot backpackSlot;
    public ItemSlot weaponSlot;
    public ItemSlot chestSlot;
    public ItemSlot bootsSlot;
    public ItemSlot legsSlot;
    public ItemSlot ringSlot;
    public ItemSlot necklaceSlot;
    public ItemSlot orbSlot;
    public ItemSlot headSlot;


    public List<Status> statuses;
    private float _currentHealth;

    public Inventory inventory;

    public virtual void LateInitialization()
    {
        if(_weaponSelector != null) _weaponSelector.DisableAllWeapons();
    }

    public void Start()
    {
        _animator = GetComponent<Animator>();
        _weaponSelector = GetComponent<WeaponSelector>();
        Initialize(this);
        statusBar = GetComponentInChildren<StatusBar>();
        statusBar.InitValues();
        characterController = GetComponent<CharacterController>();
        _controller = GetComponent<Controller>();
        InitializeSlots();
        InitializeInventory();
        currentHealth = health.Value;
        currentMana = mana.Value;
        LateInitialization();
    }

    private void InitializeInventory()
    {
        inventory = Inventory.CreateInventory(this);
    }

    private void InitializeSlots()
    {
        weaponSlot = ItemSlot.CreateSlot(this, typeof(Weapon));
        chestSlot = ItemSlot.CreateSlot(this, typeof(Chest));
        bootsSlot = ItemSlot.CreateSlot(this, typeof(Boots));
        headSlot = ItemSlot.CreateSlot(this, typeof(Head));
        legsSlot = ItemSlot.CreateSlot(this, typeof(Legs));
        necklaceSlot = ItemSlot.CreateSlot(this, typeof(Necklace));
        ringSlot = ItemSlot.CreateSlot(this, typeof(Ring));
        orbSlot = ItemSlot.CreateSlot(this, typeof(Orb));
        backpackSlot = ItemSlot.CreateSlot(this, typeof(Backpack));


        weaponSlot.PlaceItem(ScriptableObject.CreateInstance<Sword>());
        var backpack = ScriptableObject.CreateInstance<Backpack>();
        backpackSlot.PlaceItem(backpack);
        itemSlots.Add(backpackSlot);
        itemSlots.Add(weaponSlot);
        itemSlots.Add(chestSlot);
        itemSlots.Add(bootsSlot);
        itemSlots.Add(headSlot);
        itemSlots.Add(legsSlot);
        itemSlots.Add(necklaceSlot);
        itemSlots.Add(ringSlot);
        itemSlots.Add(orbSlot);
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
        inventory.UpdateInventory();
        NormalAttackCooldown();

    }

    private int? _lastWeaponHashCode;

    private void Update()
    {
        _attacking = false;
        characterController.Move(Time.fixedDeltaTime * 20f * Vector3.down);
        _controller?.Loop();
//        Move(MovingDirection);
        
        //        MovingDirection = Vector3.zero;
        Move2();
        CurrentWeaponSelect();
        _lastPosition = transform.position;
    }

    private void LateUpdate()
    {
        statusBar.SetHealthPoints(currentHealth, health.Value);
        statusBar.SetManaPoints(currentMana, mana.Value);
    }

    private void CurrentWeaponSelect()
    {
        int? tempHash = weaponSlot.itemInSlot == null ? (int?)null : weaponSlot.itemInSlot.GetHashCode();
        if (tempHash != _lastWeaponHashCode && _weaponSelector != null)
        {
            _weaponSelector.SelectWeapon(weaponSlot.itemInSlot as Weapon);
        }

        _lastWeaponHashCode = tempHash;
    }

    public Vector3 MovingDirection
    {
        get => _movingDirection;
        set => _movingDirection = value;
    }

    public Vector3 LookingDirection
    {
        get { return Vector3.ProjectOnPlane(_lookingDirection - transform.position, Vector3.up); }
        set => _lookingDirection = value;
    }

    private Vector3 _moveVector;
    private Vector3 _lastPosition;
    private Vector3 _lookVector;

    private void Move2()
    {
        Debug.DrawRay(transform.position, LookingDirection);
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        Debug.DrawRay(transform.position, _lookVector *3, Color.green);
        _lookVector = Vector3.Lerp(_lookVector.normalized,LookingDirection , 0.01f);
        AngleDiff = Vector3.SignedAngle(transform.forward, _lookVector.normalized, Vector3.up);
        float angle = Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up);
        Vector3 direction = (Quaternion.AngleAxis(-angle, Vector3.up) * MovingDirection).normalized;
        StrafeSpeed = direction.z * 3;
        ForwardSpeed = direction.x * 3;
        SpeedNormalized = movementSpeed.Value;
        Debug.DrawRay(transform.position, direction);
        MovingSpeed = MovingDirection.magnitude;
        if (MovingSpeed > 0.1f)
        {
            transform.forward = _lookVector;
        }
        //        

    }

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
        _lookVector = Vector3.ProjectOnPlane(transform.position + desiredDirection.normalized, Vector3.up);
        _lookVector.y = transform.position.y;
        Debug.DrawRay(transform.position, desiredDirection.normalized*4);
        transform.LookAt(_lookVector);
//        Debug.Log($"_lookVector = {transNform.position - _lookVector} ,dd = {desiredDirection}");

        return Vector3.SignedAngle(transform.position - _lookVector, desiredDirection, Vector3.up);
    }

    public bool IsMoving
    {
        get { return characterController.velocity.magnitude > 0.1f; }
    }

    [ReadOnly(true)]
    public float attackCooldown = 0f;

    private Vector3 target;
    private void NormalAttackCooldown()
    {
        float attackTime = 1 / attackSpeed.Value;
        if (_attacking)
        {
            if (attackCooldown > Time.fixedDeltaTime)
            {
                attackCooldown -= Time.fixedDeltaTime;
            }
            else
            {
                if (weaponSlot.itemInSlot is Weapon weapon)
                {
                    Damage damage = new Damage(physicalAttack.Value, magicPower.Value);
                    if (weapon.AttackType == AttackType.Range)
                    {
                        Projectile.CreateProjectile(weapon.ProjectileModel, damage, 60f, transform.position, target, Id);
                    }
                    else
                    {
                        
                    }
                }
             
                attackCooldown = attackTime;
            }
        }
        else
        {
            attackCooldown = attackTime;
        }
        statusBar.SetAttackCooldown(attackCooldown/attackTime);
    }

    private bool _attacking = false;

    public void TargetLook(Vector3 direction)
    {
        TurnInDirection(direction);
    }
    public void NormalAttack(Vector3 direction)
    {
        MovingDirection = Vector3.zero;

        if (!IsMoving)
        {
            Vector3 turnDirection = Vector3.ProjectOnPlane(direction, Vector3.up);
            turnDirection.y = transform.position.y;
            float angle = Mathf.Abs(TurnInDirection(turnDirection - transform.position));
            _attacking = true;
            target = direction;
            if (178f < angle && angle < 182f)
            {
                Debug.Log("NormalAttack");
            }

        }
    }

    private protected Character() { }



    private float _regenerationCounter;
    private Vector3 _movingDirection;
    private Vector3 _lookingDirection;
    private Weapon _currentWeapon;

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


