using System;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UI;

public class ItemStatsWindowBinder : Image
{
    public TextMeshProUGUI itemName;
    public GameObject typeParent;
    public TextMeshProUGUI typeValue;

    public GameObject requiredLevelParent;
    public TextMeshProUGUI requiredLevel;

    public GameObject itemAttributes;
    public GameObject itemAttributesPrefab;

}
