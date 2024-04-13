using UnityEngine;

public class FighterBullet : EnemyBullet
{
    private void OnEnable()
    {
        SoundManager.instance.PlaySFX(6);
    }
    protected override void Move()
    {
        transform.position += Vector3.down * Time.deltaTime * enemyBulletSpeed;
        base.Move();
    }
}
