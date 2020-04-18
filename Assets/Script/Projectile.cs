using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Projectile : ProjectileBase
{

    public static Projectile CreateProjectile(GameObject model, Damage damage, float speed, Vector3 spawnPos, Vector3 target, int parentId)
    {
        Projectile projectileInstance = Instantiate(model, spawnPos, Quaternion.identity).AddComponent<Projectile>();
        projectileInstance.damage = new Damage(damage.PhysicalDamage, damage.MagicalDamage, damage.OnHitEffects);
        projectileInstance.speed = speed;
        projectileInstance.gameObject.layer = 10;
        projectileInstance.direction = Vector3.ProjectOnPlane(target - spawnPos, Vector3.up).normalized;
        projectileInstance.parentId = parentId;
        return projectileInstance;
    }

    public void Start()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.velocity = direction.normalized * speed;
        rb.useGravity = false;
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }   

    private void OnTriggerEnter(Collider other)
    {
        Character characterHit = other.gameObject.GetComponent<Character>();
        if (characterHit != null)
        {
            if (characterHit.Id != parentId)
            {
                characterHit.DealDamage(damage);
                Destroy(gameObject);
                return;
            }

            return;
        }
        Destroy(gameObject);
        
//        if(!hit && other.tag.Equals("Enemy"))
//        {
//            var gameCharacter = other.GetComponent<GameCharacter>();
//            if (gameCharacter == null) return;
//            foreach (var disable in Disables)
//            {
//                gameCharacter.ApplyDisable(new Stun(gameCharacter, disable));
//            }
//            gameCharacter.ApplyDisable(new Stun(gameCharacter, 1f, 2, true));
//
////            gameCharacter.TakeDamage(damage);
//            hit = true;
//            Destroy(gameObject);
//        }
//        if (other.tag.Equals("Wall"))
//        {
//            rb.velocity = Vector3.zero;
//            Destroy(gameObject, 1f);
//        }
    }

    private void OnCollisionEnter(Collision other)
    {
//        Debug.Log("Hit");
//        Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        
    }
}

