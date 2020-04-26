using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;

    public static GameAssets Instance
    {
        get
        {
            var a =Resources.Load<GameAssets>("GameAssets");
            if (_instance == null) _instance = Instantiate(a);
            return _instance;
        }
    }

    public Transform pfDamagePopup;
}