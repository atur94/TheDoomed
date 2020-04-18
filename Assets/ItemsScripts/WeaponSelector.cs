using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    public GameObject sword;
    public GameObject bow;
    public GameObject magicStaff;
    public GameObject spear;
    public GameObject greatSword;
    public GameObject axe;
    public GameObject defaultModel;

    public void SelectWeapon(Weapon weapon)
    {
        DisableAllWeapons();

        if (weapon == null) { 
            return;
        }

        
        switch (weapon)
        {
            case Sword s:
                if(sword != null)
                    sword.SetActive(true);
                break;
            case Bow b:
                if (bow != null)
                    bow.SetActive(true);
                break;
            case GreatSword gs:
                if (greatSword != null)
                    greatSword.SetActive(true);
                break;
            case Spear s:
                if (spear != null)
                    spear.SetActive(true);
                break;
            case Staff ms:
                if (magicStaff != null)
                    magicStaff.SetActive(true);
                break;
        }
    }

    public void DisableAllWeapons()
    {
        if (sword != null) sword.SetActive(false);
        if (bow != null) bow.SetActive(false);
        if (magicStaff != null) magicStaff.SetActive(false);
        if (spear != null) spear.SetActive(false);
        if (greatSword != null) greatSword.SetActive(false);
        if (axe != null) axe.SetActive(false);
    }
}
