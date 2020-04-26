using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectorSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public Dictionary<int, Enemy> allEnemies = new Dictionary<int, Enemy>();

    private void Awake()
    {
    }
}
