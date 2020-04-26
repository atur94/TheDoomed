using System.Collections.Generic;
using UnityEngine;

public class MeleeDamageScript : DamageScriptBase
{
    [SerializeField] public List<IDamagable> objectsHit;

    private TrailRenderer trailRenderer;

    protected void Start()
    {
        base.Start();
        objectsHit = new List<IDamagable>();
        weaponHandlerTransform = GetWeaponHandler(transform);

        trailRenderer = gameObject.AddComponent<TrailRenderer>();
        trailRenderer.startWidth = 2f;
        trailRenderer.endWidth = 0f;
        trailRenderer.time = 0.5f;
        trailRenderer.emitting = false;
    }

    public override void Attack(Collider other)
    {
        if (other.gameObject.GetComponent<IDamagable>() is IDamagable damagable && !objectsHit.Contains(damagable) &&
            CanDealDamage)
        {
            if (damagable is Character character && character.Id == Character.Id) return;
            objectsHit.Add(damagable);
            Character.NormalAttackDamage(damagable);
        }
    }

    void Update()
    {
        if (CanDealDamage)
        {
            var pos0 = Character.transform.position;
            var pos1 = weaponHandlerTransform.position - pos0;

            for (int i = 0; i < raysNumber; i++)
            {
                pos0.y = _raysHeight / raysNumber * i;

                Ray ray = new Ray(pos0, new Vector3(pos1.x, 0, pos1.z));
                Debug.DrawRay(ray.origin, ray.direction * range);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, range))
                {
                    Attack(hit.collider);
                }
            }
        }
    }
    // wywolane z animacji
    public void MeleeAttackStart()
    {
        if (Character == null)
        {
            CanDealDamage = false;
            return;
        }

        objectsHit.Clear();
        trailRenderer.emitting = true;
        CanDealDamage = true;
    }

    public void MeleeAttackEnd()
    {
        if (Character == null) return;
        CanDealDamage = false;
        objectsHit.Clear();
        trailRenderer.emitting = false;
    }

}