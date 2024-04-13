using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlecruiserLaserBeam : MonoBehaviour
{
    [SerializeField] float enemyBulletDamage;

    // 플레이어와 충돌했을 때 플레이어에게 데미지
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StatesManager.instance.ChangeHP(enemyBulletDamage);
            StatesManager.instance.ChangeMaterial();
            StatesManager.instance.Death();
        }
    }
}
