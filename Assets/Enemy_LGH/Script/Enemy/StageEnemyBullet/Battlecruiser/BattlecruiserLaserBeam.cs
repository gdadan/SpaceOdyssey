using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlecruiserLaserBeam : MonoBehaviour
{
    [SerializeField] float enemyBulletDamage;

    // �÷��̾�� �浹���� �� �÷��̾�� ������
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
