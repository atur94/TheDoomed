using System.Collections;
using System.Collections.ObjectModel;
using UnityEngine;

public class DefaultWeaponPicker : MonoBehaviour
{
    private Character _character;

    public Weapon defaultWeapon;

    void Start()
    {
        _character = GetComponent<Character>();
        StartCoroutine(PickWeapons());
    }

    IEnumerator PickWeapons()
    {
        yield return new WaitForEndOfFrame();
        if (defaultWeapon != null && _character != null) _character.weaponSlot.ItemInSlot = defaultWeapon;

    }
}
