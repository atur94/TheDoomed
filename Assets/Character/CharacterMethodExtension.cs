using UnityEngine;
public abstract partial class Character
{
    public void Collect(Pickable pickable)
    {
        foreach (var eqSlot in itemSlots)
        {
            if (eqSlot != null && eqSlot.itemInSlot == null && eqSlot.itemTypeRestriction == pickable._item.GetType() && eqSlot.CanBePlaced(pickable._item))
            {
                eqSlot.PlaceItemInEquipment(pickable._item);
                return;
            }
        }
        inventory.PutItemToInventory(pickable._item);
    }

    private float _angle;
    private float _angleDiff;
    private float _movingSpeed;
    private float _strafeSpeed;
    private float _forwardSpeed;
    private float _speedNormalized;
    private static readonly int AngleDiffAnimator = Animator.StringToHash("AngleDiff");
    private static readonly int Speed = Animator.StringToHash("MovingSpeed");
    private static readonly int StrafeSpeedAnimator = Animator.StringToHash("StrafeSpeed");
    private static readonly int ForwardSpeedAnimator = Animator.StringToHash("ForwardSpeed");
    private static readonly int SpeedNormalizedAnimator = Animator.StringToHash("SpeedNormalized");

    public float Angle
    {
        get => _animator.GetFloat(2);
        set
        {
            _angle = value;
            _animator.SetFloat(2, _angle);
        }
    }

    public float AngleDiff
    {
        get => _angleDiff;
        set
        {
            _angleDiff = value;
            _animator.SetFloat(AngleDiffAnimator, _angleDiff);

        }
    }

    public float StrafeSpeed
    {
        get => _strafeSpeed;
        set
        {
            _strafeSpeed = value;
            _animator.SetFloat(StrafeSpeedAnimator, _strafeSpeed);
        }
    }

    public float ForwardSpeed
    {
        get => _forwardSpeed;
        set
        {
            _forwardSpeed = value;
            _animator.SetFloat(ForwardSpeedAnimator, _forwardSpeed);
        }
    }

    public float MovingSpeed
    {
        get => _movingSpeed;
        set
        {
            _movingSpeed = value;
            _animator.SetFloat(Speed, _movingSpeed);
        }
    }
    //150 wartość 1
    public float SpeedNormalized
    {
        get => _speedNormalized;
        set
        {
            _speedNormalized = value / 150f;
            _animator.SetFloat(SpeedNormalizedAnimator, _speedNormalized);
        }
    }
}