using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private KeyValuePair<int, string> _group = new KeyValuePair<int, string>(100, "Monsters");

    public override KeyValuePair<int, string> @group => _group;

    public override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown("l"))
        {
            TakeDamage(new Damage(25,25, null));
        }
    }
}