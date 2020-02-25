using System;
using JetBrains.Annotations;
using UnityEngine;

public class Projectile : ProjectileBase
{
    public Projectile(int damage, float speed, Vector3 direction) : base(damage, speed, direction)
    {
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = direction * speed;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Enemy"))
        {
            var gameCharacter = other.GetComponent<GameCharacter>();
            if (gameCharacter == null) return;
            gameCharacter.TakeDamage(damage);
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

