using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattlecruiserCircleShotShooter : MonoBehaviour
{
    public EnemyBullet circleShotBullet;

    public bool left;

    int bulletNum = 5;

    float radius = 0.5f;
    float angle;

    private void OnEnable()
    {
        StartCoroutine(CircleShotBullet());
    }

    IEnumerator CircleShotBullet()
    {
        if (left)
        {
            for (int i = bulletNum; i > -1; i--)
            {
                angle = i * -Mathf.PI / bulletNum;

                EnemyBullet eb = Instantiate(circleShotBullet, transform);
                eb.transform.position = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                eb.transform.rotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);

                yield return new WaitForSeconds(1f);
            }
        }
        else
        {
            for (int i = 0; i < bulletNum + 1; i++)
            {
                angle = i * -Mathf.PI / bulletNum;

                EnemyBullet eb = Instantiate(circleShotBullet, transform);
                eb.transform.position = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                eb.transform.rotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);

                yield return new WaitForSeconds(1f);
            }
        }

        yield return new WaitForSeconds(1.5f);
        foreach (BattlecruiserCircleShotBullet csb in transform.GetComponentsInChildren<BattlecruiserCircleShotBullet>())
        {
            csb.canMove = true;
            csb.transform.SetParent(EnemyBulletManager.instance.gameObject.transform);
            SoundManager.instance.PlaySFX(9);
        }

        yield return new WaitForSeconds(0.5f);
        transform.parent.gameObject.SetActive(false);
    }
}
