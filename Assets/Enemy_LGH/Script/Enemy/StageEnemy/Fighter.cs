using UnityEngine;

public class Fighter : Enemy
{
    // Fighter�� �̵� ���
    // ���� ���� �������ٰ� �¿�� �����δ�

    // Fighter�� ���� ���
    // �¿�� ������ ���� �Ѿ��� �߻��Ѵ�
    // �Ѿ��� ������ ���ϴ�

    [SerializeField] float fighterSpeed = 3f;         // Fighter ���ǵ�
    [SerializeField] float fighterDamage = 5f;        // Fighter ������

    float stayYAxis;                // ���� Y��ǥ
    float endBoundary = 2f;         // ������ X��ǥ�� ��
    float shotTime = 1f;            // ���� ��Ÿ��

    Vector3 attackMoveVec;

    public EnemyBullet stage1FighterBullet;

    protected override void Move()
    {
        transform.position += Vector3.down * Time.deltaTime * fighterSpeed;

        // Fighter�� y�� ��ǥ�� ��ġ �̸����� �������� ���ݽ���
        if (transform.position.y < stayYAxis)
        {
            attackMoveVec = Vector3.right * Time.deltaTime * fighterSpeed;
            state = State.Attack;
        }

        base.Move();
    }

    protected override void Attack()
    {
        // Attack ������ �� �¿�� ������
        transform.position += attackMoveVec;

        if (transform.position.x < -endBoundary) attackMoveVec = Vector3.right * Time.deltaTime * fighterSpeed;
        else if (transform.position.x > endBoundary) attackMoveVec = Vector3.left * Time.deltaTime * fighterSpeed;

        Shoot();
        base.Attack();
    }

    protected override void ChildStart()
    {
        type = EnemyType.Nomal;
        // Y�� ��ǥ�� ���� �ȿ��� ��������
        stayYAxis = Random.Range(0f, 4f);

        base.ChildStart();
    }

    void Shoot()
    {
        // 1�ʸ��� ���� �Ѿ��� �������� ���ư�
        shotTime += Time.deltaTime;
        if (shotTime >= 1f)
        {
            EnemyBullet enemyBullet = Instantiate(stage1FighterBullet, EnemyBulletManager.instance.gameObject.transform);
            EnemyBulletManager.instance.enemyBullets.Add(enemyBullet);

            enemyBullet.enemyBulletDamage = fighterDamage;
            enemyBullet.transform.position = transform.position;

            shotTime = 0;
        }
    }
}
