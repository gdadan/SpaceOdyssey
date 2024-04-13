using System.Collections;
using UnityEngine;

public class BattlecruiserMissileLauncher : MonoBehaviour
{
    public EnemyBullet missile;

    [SerializeField] int missileTime = 1;

    private void OnEnable()
    {
        StartCoroutine(LaunchMissile());
    }

    IEnumerator LaunchMissile()
    {
        for (int i = 0; i < missileTime; i++)
        {
            EnemyBullet bullet = Instantiate(missile, EnemyBulletManager.instance.gameObject.transform);
            EnemyBulletManager.instance.enemyBullets.Add(bullet);

            bullet.transform.eulerAngles += transform.parent.parent.eulerAngles;
            bullet.transform.position = transform.position;

            yield return new WaitForSeconds(0.2f);
        }
        gameObject.SetActive(false);
    }
}
