using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentList : MonoBehaviour
{
    private static EquipmentList _instance;
    public List<Sword> swords;
    public List<Axe> axes;
    public List<GreatSword> greatSwords;
    public List<Staff> staffs;
    public List<Spear> spears;
    public List<Bow> bows;

    public static EquipmentList Instance
    {
        get
        {
            if (_instance == null) _instance = Instantiate(Resources.Load<EquipmentList>("EquipmentList"));
            return _instance;
        }
    }

    void Start()
    {
        swords = Resources.LoadAll<Sword>("Items/Weapons/Swords").ToList();
        axes = Resources.LoadAll<Axe>("Items/Weapons/Axes").ToList();
        greatSwords = Resources.LoadAll<GreatSword>("Items/Weapons/GreatSwords").ToList();
        staffs = Resources.LoadAll<Staff>("Items/Weapons/Staffs").ToList();
        spears = Resources.LoadAll<Spear>("Items/Weapons/Spears").ToList();
        bows = Resources.LoadAll<Bow>("Items/Weapons/Bows").ToList();
    }
}