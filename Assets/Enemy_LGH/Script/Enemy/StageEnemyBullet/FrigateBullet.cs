using UnityEngine;

public class FrigateBullet : EnemyBullet
{
    private void OnEnable()
    {
        SoundManager.instance.PlaySFX(6);
    }

    protected override void Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * enemyBulletSpeed);
        base.Move();
    }
}
