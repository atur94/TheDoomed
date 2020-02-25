using System;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public int damage;
    public float speed;
    public Vector3 direction;

    public Rigidbody rb;

    public ProjectileBase(int damage, float speed, Vector3 direction)
    {
        this.damage = damage;
        this.speed = speed;
        this.direction = direction;
    }
}

[Serializable]
public struct ProjectileType
{
    public int damage;
    public float speed;
    public Vector3 direction;
}
