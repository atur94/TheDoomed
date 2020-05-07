using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;


public interface IDamagable
{
    void TakeDamage(Damage damage);
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

    public Inventory inventory;

    public virtual void LateInitialization()
    {
        if(_weaponSelector != null)
        {
            _weaponSelector.DisableAllWeapons();
            _weaponSelector.SelectWeapon(weaponSlot.ItemInSlot as Weapon);
            UpdateAllStats();
        }
    }

    public virtual void Start()
    {
        _animator = GetComponent<Animator>();
        _weaponSelector = transform.GetComponent<WeaponSelector>();
        Initialize(this);
        statusBar = transform.GetComponentInChildren<StatusBar>();
        if (statusBar != null)
        {
            statusBar.InitValues();
        }
        characterController = GetComponent<CharacterController>();
        _controller = GetComponent<Controller>();
        InitializeSlots();
        InitializeInventory();
        CurrentHealth = health.Value;
        CurrentMana = mana.Value;
        LateInitialization();
    }

    private void InitializeInventory()
    {
        inventory = Inventory.CreateInventory(this);
    }

    private void InitializeSlots()
    {
        weaponSlot = ItemSlot.CreateSlot(this, typeof(Weapon));
        weaponSlot.PropertyChanged += CurrentWeaponSelect2;
        weaponSlot.PropertyChanged += SlotOnPropertyChanged;

        chestSlot = ItemSlot.CreateSlot(this, typeof(Chest));
        chestSlot.PropertyChanged += SlotOnPropertyChanged;

        bootsSlot = ItemSlot.CreateSlot(this, typeof(Boots));
        bootsSlot.PropertyChanged += SlotOnPropertyChanged;

        headSlot = ItemSlot.CreateSlot(this, typeof(Head));
        headSlot.PropertyChanged += SlotOnPropertyChanged;

        legsSlot = ItemSlot.CreateSlot(this, typeof(Legs));
        legsSlot.PropertyChanged += SlotOnPropertyChanged;

        necklaceSlot = ItemSlot.CreateSlot(this, typeof(Necklace));
        necklaceSlot.PropertyChanged += SlotOnPropertyChanged;

        ringSlot = ItemSlot.CreateSlot(this, typeof(Ring));
        ringSlot.PropertyChanged += SlotOnPropertyChanged;

        orbSlot = ItemSlot.CreateSlot(this, typeof(Orb));
        orbSlot.PropertyChanged += SlotOnPropertyChanged;

        backpackSlot = ItemSlot.CreateSlot(this, typeof(Backpack));
        backpackSlot.PropertyChanged += SlotOnPropertyChanged;

        var sword = ScriptableObject.CreateInstance<Sword>();
        var boots = ScriptableObject.CreateInstance<Boots>();
        boots.MovementSpeed = new AttributeSet(AttributeType.MovementSpeed, 30f, 0.1f);
        sword.AttackSpeed = new AttributeSet(AttributeType.AttackSpeed, 1.3f, 0);
        sword.PhysicalAttack = new AttributeSet(AttributeType.PhysicalAttack, 35f, 0);
        weaponSlot.PlaceItem(sword);
        bootsSlot.PlaceItem(boots);
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

    private void UpdateAllStats()
    {
        foreach (var commonAttribute in attributes)
        {
            commonAttribute.IsChanged = true;
        }
    }
    private void SlotOnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        UpdateAllStats();
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

    public void TakeDamage(Damage damage)
    {
        double physicalDamageReduction = 1 - (0.052 * physicalDefense.Value) / (0.9 + 0.048 * Mathf.Abs(physicalDefense.Value));
        double magicalDamageReduction = 1 - (0.052 * magicalDefense.Value) / (0.9 + 0.048 * Mathf.Abs(magicalDefense.Value));

        Random critRandom = new Random();
        bool isCrit = critRandom.Next(0, 100) <= damage.damageDealer.criticalChance.PercentBonuses * 100;
        float critDamage = isCrit ? damage.damageDealer.criticalDamage.Value : 0;
        int calculatedDamage = (int)((physicalDamageReduction * damage.PhysicalDamage +
                                      magicalDamageReduction * damage.MagicalDamage) * (1 + critDamage));
        DamagePopup.Create(Camera.main.WorldToScreenPoint(transform.position), calculatedDamage, isCrit);
        if (CurrentHealth > calculatedDamage)
        {
            CurrentHealth -= calculatedDamage;
        }
        else
        {
            CurrentHealth = 0;
            Destroy(gameObject);
        }
    }

    public void NormalAttackDamage(IDamagable damagable)
    {
        damagable.TakeDamage(new Damage(physicalAttack.Value, 0, this));
    }

    public void BurnMana(float manaToBurn)
    {
        if (CurrentMana > manaToBurn)
        {
            CurrentMana -= manaToBurn;
        }
    }

    private void FixedUpdate()
    {
        disables.Reset();
        RegenerationPerSecond();
        inventory.UpdateInventory();
        NormalAttackCooldown();
//        CurrentWeaponSelect();
        _isCollecting = false;

    }

    private int? _lastWeaponHashCode;

    protected virtual void Update()
    {
        //        Attacking = false;
        characterController.Move(Time.fixedDeltaTime * 20f * Vector3.down);
        
        //        MovingDirection = Vector3.zero;
        Move();
        _lastPosition = transform.position;
    }

    private void CurrentWeaponSelect2(object sender, PropertyChangedEventArgs e)
    {
        var slot = sender as ItemSlot;
        if(slot != null && _weaponSelector != null)
            _weaponSelector.SelectWeapon(slot.ItemInSlot as Weapon);
    }

    private void CurrentWeaponSelect()
    {
        int? tempHash = weaponSlot.ItemInSlot == null ? (int?)null : weaponSlot.ItemInSlot.GetHashCode();
        if (tempHash != _lastWeaponHashCode && _weaponSelector != null)
        {
            _weaponSelector.SelectWeapon(weaponSlot.ItemInSlot as Weapon);
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
    public Vector3 CurrentLookVector;

    private void Move()
    {
        Debug.DrawRay(transform.position, LookingDirection, Color.magenta);
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        Debug.DrawRay(transform.position, CurrentLookVector *3, Color.green);
        CurrentLookVector = Vector3.Lerp(CurrentLookVector.normalized,LookingDirection , 0.01f);
        AngleDiff = Vector3.SignedAngle(transform.forward, CurrentLookVector.normalized, Vector3.up);
        float angle = Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up);
        Vector3 direction = (Quaternion.AngleAxis(-angle, Vector3.up) * MovingDirection).normalized;
        StrafeSpeed = direction.z * 3;
        ForwardSpeed = direction.x * 3;
        SpeedNormalized = movementSpeed.Value;
        Debug.DrawRay(transform.position, direction);
        MovingSpeed = MovingDirection.magnitude;

        if (MovingSpeed > 0.1f)
        {
            transform.forward = CurrentLookVector;
        }
        //        

    }

    public bool IsMoving
    {
        get { return characterController.velocity.magnitude > 0.1f; }
    }

    [ReadOnly(true)]
    public float attackCooldown = 0f;

    private void NormalAttackCooldown()
    {
        float attackTime = 1 / attackSpeed.Value;
        if (Attacking && attackSpeed.Value > 0)
        {
            if (attackCooldown > Time.fixedDeltaTime)
            {
                attackCooldown -= Time.fixedDeltaTime;
                transform.forward = CurrentLookVector;
                SelectWeaponTypeUpdate();
                var states = _animator.GetCurrentAnimatorClipInfo(0);
                
                if(states.Length > 0)
                {
                    if (attackTime == 0f)
                    {

                    }
                    AttackSpeedModifier = states[0].clip.length / attackTime;
                }
//                AttackSpeedModifier = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / attackTime;

            }
            else
            {
                if (weaponSlot.ItemInSlot is Weapon weapon)
                {
//                    Damage damage = new Damage(physicalAttack.Value, magicPower.Value);
//                    if (weapon.AttackType == AttackType.Range)
//                    {
//                       Projectile.CreateProjectile(weapon.ProjectileModel, damage, 60f, transform.predictedPosition, target, Id);
//                    }
//                    else
//                    {
//                        
//                    }
                }
                attackCooldown = attackTime;
            }
        }
        else
        {
            attackCooldown = attackTime;
            _animator.StopPlayback();
        }
        if(statusBar != null)
            statusBar.SetAttackCooldown(attackCooldown/attackTime);
    }

    public void NormalAttack(Vector3 direction)
    {
        if (!_isCollecting)
        {
            MovingDirection = Vector3.zero;

            if (!IsMoving && weaponSlot.ItemInSlot != null)
            {
                Attacking = true;
            }
        }
        


    }

    private protected Character() { }



    private float _regenerationCounter;
    private Vector3 _movingDirection;
    private Vector3 _lookingDirection;
    private Weapon _currentWeapon;
    private bool _attacking;

    private void RegenerationPerSecond()
    {
        //Invoke in FixedUpdate
        if(_regenerationCounter > 1f)
        {
            if (CurrentHealth <= health.Value && CurrentHealth > 0)
            {
                if ((CurrentHealth + healthRegen.Value) < health.Value)
                {
                    CurrentHealth += healthRegen.Value;
                }
                else
                {
                    CurrentHealth = health.Value;
                }
            }

            if (CurrentMana <= mana.Value && CurrentMana > 0)
            {
                if (CurrentMana + manaRegen.Value < mana.Value)
                {
                    CurrentMana += manaRegen.Value;
                }
                else
                {
                    CurrentMana = mana.Value;
                }
            }

            _regenerationCounter = 0;
        }

        _regenerationCounter += Time.fixedDeltaTime;
    }

    private void SelectWeaponTypeUpdate()
    {
        if (weaponSlot.ItemInSlot == null)
        {
            CurrentWeaponType = (int)Weapon.WeaponType.NoWeapon;
            return;
        }

        switch (weaponSlot.ItemInSlot)
        {
            case Sword _:
                CurrentWeaponType = (int) Weapon.WeaponType.Sword;
                break;
            case Bow _:
                CurrentWeaponType = (int) Weapon.WeaponType.Bow;
                break;
            case GreatSword _:
                CurrentWeaponType = (int)Weapon.WeaponType.GreatSword;
                break;
            case Spear _:
                CurrentWeaponType = (int)Weapon.WeaponType.Spear;
                break;
            case Staff _:
                CurrentWeaponType = (int)Weapon.WeaponType.Staff;
                break;
            case Axe _:
                CurrentWeaponType = (int)Weapon.WeaponType.Axe;
                break;
        }
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName.Equals("CurrentHealth"))
        {
            if (statusBar == null) return;
            statusBar.SetHealthPoints(CurrentHealth, health.Value);
        }

        if (propertyName.Equals("CurrentMana"))
        {
            if (statusBar == null) return;
            statusBar.SetManaPoints(CurrentMana, mana.Value);
        }
    }
}


