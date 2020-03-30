using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public override void LateInitialization()
    {
        base.LateInitialization();
        FindObjectOfType<PlayerUIController>().BindUIToCharacter(this);
    }

    private KeyValuePair<int, string> _group = new KeyValuePair<int, string>(1, "Player");

    public override KeyValuePair<int, string> @group => _group;
}