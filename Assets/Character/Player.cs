using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private KeyValuePair<int, string> _group = new KeyValuePair<int, string>(1, "Player");

    public override KeyValuePair<int, string> @group => _group;
}