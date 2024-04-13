using System.Collections;
using UnityEngine;

public class BattlecruiserCircleShotBullet : EnemyBullet
{
    SpriteRenderer rend;

    float gamma = 0;

    public bool canMove = false;

    private void OnEnable()
    {
        StartCoroutine(Release());
        SoundManager.instance.PlaySFX(5);
    }

    private void Update()
    {
        if (canMove) transform.Translate(Vector3.right * Time.deltaTime * enemyBulletSpeed);
    }

    IEnumerator Release()
    {
        rend = GetComponent<SpriteRenderer>();
        while (gamma < 1)
        {
            rend.color = new Color(255, 255, 255, gamma);
            gamma += Time.deltaTime * 20;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
