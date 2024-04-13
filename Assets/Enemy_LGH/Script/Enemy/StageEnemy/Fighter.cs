using UnityEngine;

public class Fighter : Enemy
{
    // Fighter의 이동 방식
    // 일정 정도 내려오다가 좌우로 움직인다

    // Fighter의 공격 방식
    // 좌우로 움직일 때에 총알을 발사한다
    // 총알은 빠르고 약하다

    [SerializeField] float fighterSpeed = 3f;         // Fighter 스피드
    [SerializeField] float fighterDamage = 5f;        // Fighter 데미지

    float stayYAxis;                // 멈출 Y좌표
    float endBoundary = 2f;         // 움직일 X좌표의 끝
    float shotTime = 1f;            // 공격 쿨타임

    Vector3 attackMoveVec;

    public EnemyBullet stage1FighterBullet;

    protected override void Move()
    {
        transform.position += Vector3.down * Time.deltaTime * fighterSpeed;

        // Fighter의 y축 좌표가 수치 미만으로 내려가면 공격시작
        if (transform.position.y < stayYAxis)
        {
            attackMoveVec = Vector3.right * Time.deltaTime * fighterSpeed;
            state = State.Attack;
        }

        base.Move();
    }

    protected override void Attack()
    {
        // Attack 상태일 때 좌우로 움직임
        transform.position += attackMoveVec;

        if (transform.position.x < -endBoundary) attackMoveVec = Vector3.right * Time.deltaTime * fighterSpeed;
        else if (transform.position.x > endBoundary) attackMoveVec = Vector3.left * Time.deltaTime * fighterSpeed;

        Shoot();
        base.Attack();
    }

    protected override void ChildStart()
    {
        type = EnemyType.Nomal;
        // Y축 좌표는 범위 안에서 랜덤으로
        stayYAxis = Random.Range(0f, 4f);

        base.ChildStart();
    }

    void Shoot()
    {
        // 1초마다 공격 총알은 직선으로 날아감
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
