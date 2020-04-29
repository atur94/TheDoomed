public abstract class RangeWeaponBase : Weapon
{
    private AttributeSet _bulletSpeed;
    public AttributeSet BulletSpeed
    {
        get { return _bulletSpeed; }
        set { _bulletSpeed = value; AddToList(value); }
    }
}

