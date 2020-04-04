using System;
using UnityEngine;


public class Pickable : MonoBehaviour, IPickable
{
    public Item _item;
    public float radius;
    [SerializeField]
    private Character _character;

    public void Pick()
    {
        _character.Collect(this);
        Destroy(gameObject);
        
    }

    private void Awake()
    {
        radius = 2f;
        var gm = FindObjectOfType<GameManager>();
        _character = gm.controllableCharacter;
    }

    private void OnMouseDown()
    {
        float distance = Vector3.Distance(_character.transform.position, transform.position);
        if (distance < 2)
        {
            Pick();
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(1f, 1f,0, 0.3f);
        Gizmos.DrawSphere(transform.position, radius);
    }

}
