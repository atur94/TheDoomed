using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public Character controllableCharacter;
    public Texture2D CursorTexture;
    public bool aIEnabled = true;

    public List<Character> PlayersList { get; private set; }

    void Awake()
    {
        EquipmentList.Instance.name = "Lista przedmiotów";
    }
    // Use this for initialization
    void Start()
    {
//        Cursor.SetCursor(CursorTexture, Vector3.zero,CursorMode.Auto);
        Physics.IgnoreLayerCollision(8,13);
        Physics.IgnoreLayerCollision(9,13);
        controllableCharacter = FindObjectOfType<Player>();
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        PlayersList = new List<Character>();

    }
}
