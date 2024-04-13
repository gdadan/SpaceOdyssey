using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    public static EnemyBulletManager instance;

    public List<EnemyBullet> enemyBullets;

    private void Awake()
    {
        instance = this;
    }
}
