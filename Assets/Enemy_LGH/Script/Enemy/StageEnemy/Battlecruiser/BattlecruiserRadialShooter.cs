using System.Collections;
using UnityEngine;

public class BattlecruiserRadialShooter : MonoBehaviour
{
    public EnemyBullet radialBullet;

    private void OnEnable()
    {
        StartCoroutine(RadialBullet());
    }

    IEnumerator RadialBullet()
    {
        for (int i = 0; i < 20; i++)
        {
            EnemyBullet bullet = Instantiate(radialBullet, EnemyBulletManager.instance.gameObject.transform);
            EnemyBulletManager.instance.enemyBullets.Add(bullet);

            bullet.transform.position = transform.position;
            yield return new WaitForSeconds(0.5f);
        }
        gameObject.SetActive(false);
    }
}
