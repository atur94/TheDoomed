using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Character controllableCharacter;
    public Texture2D CursorTexture;

    // Use this for initialization
    void Start()
    {
        Cursor.SetCursor(CursorTexture, Vector3.zero,CursorMode.Auto);

        controllableCharacter = FindObjectOfType<Player>();
    }
}
