using System;
using UnityEngine;


public class Pickable : MonoBehaviour, IPickable
{
    public Item _item;
    public float radius;
    private GameManager gm;

    public void Pick()
    {
        gm.controllableCharacter.Collect(this);
        Destroy(gameObject);
        
    }

    private void Awake()
    {
        radius = 3f;
    }

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        var rb = gameObject.GetComponent<Rigidbody>();

        if (rb == null) gameObject.AddComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
            float distance = Vector3.Distance(gm.controllableCharacter.transform.position, transform.position);
        if (distance < 2)
        {
            Pick();
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's predictedPosition
        Gizmos.color = new Color(1f, 1f,0, 0.3f);
        Gizmos.DrawSphere(transform.position, radius);
    }

}
