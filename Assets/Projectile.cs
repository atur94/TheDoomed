using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Projectile : ProjectileBase
{
    public Projectile(int damage, float speed, Vector3 direction) : base(damage, speed, direction)
    {

    }

    public Projectile(ProjectileType projectileType) : base(projectileType.damage, projectileType.speed, projectileType.direction) { }

    public static Projectile CreateProjectile(GameObject where, ProjectileType projectileType)
    {
        Projectile projectile = where.AddComponent<Projectile>();
        projectile.damage = projectileType.damage;
        projectile.speed = projectileType.speed;
        projectile.direction = projectileType.direction;
        projectile.Disables = new List<Disable>();
        return projectile;
    }
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!hit && other.tag.Equals("Enemy"))
        {
            var gameCharacter = other.GetComponent<GameCharacter>();
            if (gameCharacter == null) return;
            foreach (var disable in Disables)
            {
                gameCharacter.ApplyDisable(new Stun(gameCharacter, disable));
            }
            gameCharacter.ApplyDisable(new Stun(gameCharacter, 1f, 2, true));

            gameCharacter.TakeDamage(damage);
            hit = true;
            Destroy(gameObject);
        }
        if (other.tag.Equals("Wall"))
        {
            rb.velocity = Vector3.zero;
            Destroy(gameObject, 1f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit");
    }

    public void FixedUpdate()
    {
    }
}

