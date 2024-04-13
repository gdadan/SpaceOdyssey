using UnityEngine;

public class BattlecruiserRadialBullet : EnemyBullet
{
    float rotateAngle = 0f;

    [SerializeField] float rotation = 50f;
    [SerializeField] float reduceSpeed = 10f;

    private void OnEnable()
    {
        SoundManager.instance.PlaySFX(6);
    }

    private void Start()
    {
        enemyBulletDamage = 10f;
    }

    protected override void Move()
    {
        transform.Translate(Vector2.up * Time.deltaTime * enemyBulletSpeed);
        transform.rotation = Quaternion.Euler(0, 0, rotateAngle * rotation);
        rotateAngle -= Time.deltaTime;
        enemyBulletSpeed += Time.deltaTime / reduceSpeed;
        base.Move();
    }
}
