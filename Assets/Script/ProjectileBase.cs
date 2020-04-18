using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    public Damage damage;
    public float speed;
    public Vector3 direction;
    public bool hit { get; set; }
    protected List<Status> Disables;
    public int parentId;
    public Rigidbody rb;
}

[Serializable]
public struct ProjectileType
{
    public int damage;
    public float speed;
    public Vector3 direction;
}
