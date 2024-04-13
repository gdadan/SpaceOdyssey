using UnityEngine;

public class Frigate : Enemy
{
    // Frigate 이동방식
    // 스폰 후 플레이어 방향으로 이동

    // Frigate 공격방식
    // 공격범위 안에 플레이어가 들어오면
    // 이동을 멈추고 플레이어를 향해 총알을 발사한다.

    [SerializeField] float frigateSpeed = 1f;          // Frigate 스피드
    [SerializeField] float frigateDamage = 20f;        // Frigate 데미지

    float shotTime = 2f;

    public EnemyBullet frigateBullet;

    protected override void ChildStart()
    {
        type = EnemyType.Nomal;

        base.ChildStart();
    }

    // 플레이어를 향해 다가옴
    protected override void Move()
    {
        LookPlayer();
        transform.Translate(Vector3.down * Time.deltaTime * frigateSpeed);

        base.Move();
    }

    // 이동을 멈추고 플레이어를 향해 사격함
    protected override void Attack()
    {
        LookPlayer();
        Shoot();
        base.Attack();
    }

    // 2초마다 플레이어에게 사격
    void Shoot()
    {
        shotTime += Time.deltaTime;
        if (shotTime >= 2)
        {
            EnemyBullet enemyBullet = Instantiate(frigateBullet, EnemyBulletManager.instance.gameObject.transform);
            EnemyBulletManager.instance.enemyBullets.Add(enemyBullet);

            enemyBullet.enemyBulletDamage = frigateDamage;
            enemyBullet.transform.position = transform.position;
            enemyBullet.transform.rotation = transform.rotation;

            shotTime = 0;
        }
    }
}
