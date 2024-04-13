using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public List<Enemy> enemies;

    private void Awake()
    {
        instance = this;
    }
}
