using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private KeyValuePair<int, string> _group = new KeyValuePair<int, string>(100, "Monsters");

    public override KeyValuePair<int, string> @group => _group;
}