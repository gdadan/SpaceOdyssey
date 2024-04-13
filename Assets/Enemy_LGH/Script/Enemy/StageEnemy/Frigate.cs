using UnityEngine;

public class Frigate : Enemy
{
    // Frigate �̵����
    // ���� �� �÷��̾� �������� �̵�

    // Frigate ���ݹ��
    // ���ݹ��� �ȿ� �÷��̾ ������
    // �̵��� ���߰� �÷��̾ ���� �Ѿ��� �߻��Ѵ�.

    [SerializeField] float frigateSpeed = 1f;          // Frigate ���ǵ�
    [SerializeField] float frigateDamage = 20f;        // Frigate ������

    float shotTime = 2f;

    public EnemyBullet frigateBullet;

    protected override void ChildStart()
    {
        type = EnemyType.Nomal;

        base.ChildStart();
    }

    // �÷��̾ ���� �ٰ���
    protected override void Move()
    {
        LookPlayer();
        transform.Translate(Vector3.down * Time.deltaTime * frigateSpeed);

        base.Move();
    }

    // �̵��� ���߰� �÷��̾ ���� �����
    protected override void Attack()
    {
        LookPlayer();
        Shoot();
        base.Attack();
    }

    // 2�ʸ��� �÷��̾�� ���
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
