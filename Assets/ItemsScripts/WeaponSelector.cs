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
    public GameObject leftHand;
    public GameObject rightHand;
    private GameObject _modelInstance;
    public void SelectWeapon(Weapon weapon)
    {
        DisableAllWeapons();
        if (_modelInstance != null) Destroy(_modelInstance);

        if (weapon == null) { 
            return;
        }

        
        switch (weapon)
        {
            case Sword s:
                if(sword != null)
                {
                    sword.SetActive(true);
                    SelectModel(sword, weapon);
                    if (_modelInstance != null) _modelInstance.AddComponent<MeleeDamageScript>();
                }
                break;
            case Bow b:
                if (bow != null)
                {
                    bow.SetActive(true);
                    SelectModel(bow, weapon);
                    if(_modelInstance != null) _modelInstance.AddComponent<RangeDamageScript>();
                }
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
                magicStaff.SetActive(true);
                SelectModel(magicStaff, weapon);
                if (_modelInstance != null) _modelInstance.AddComponent<RangeDamageScript>();
                break;
        }
    }

    private void SelectModel(GameObject parent, Weapon weapon)
    {
        var model = weapon.itemModel ? weapon.itemModel : defaultModel;
        _modelInstance = Instantiate(model, Vector3.zero, Quaternion.identity, parent.transform);
        _modelInstance.transform.localPosition = model.transform.position;
        _modelInstance.transform.localRotation = model.transform.rotation;
        _modelInstance.GetComponentInChildren<MeshCollider>();
        var coll = _modelInstance.GetComponentInChildren<MeshCollider>();
        if(coll != null) Destroy(coll);
    }

    public void DisableAllWeapons()
    {
        if(_modelInstance != null) Destroy(_modelInstance.gameObject);
        if (sword != null) sword.SetActive(false);
        if (bow != null) bow.SetActive(false);
        if (magicStaff != null) magicStaff.SetActive(false);
        if (spear != null) spear.SetActive(false);
        if (greatSword != null) greatSword.SetActive(false);
        if (axe != null) axe.SetActive(false);
    }
}
