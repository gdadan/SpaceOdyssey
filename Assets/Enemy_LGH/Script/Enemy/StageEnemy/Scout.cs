using UnityEngine;

public class Scout : Enemy
{
    // Scout 이동 방식
    // 아래로 이동(플레이어 방향으로 이동할 수도 있고 아닐 수도 있다)
    // 플레이어에게 이동할 때에는 Enemy의 LookPlayer를 이용하여 이동한다.

    // Scout 공격 방식
    // 플레이어에게 부딪히면 데미지를 준다.
    // 그 외 다른 공격 방식은 없다.

    [SerializeField] float scoutSpeed = 5f;          // Scout 스피드
    [SerializeField] float scoutDamage = 10f;        // Scout 데미지

    protected override void ChildStart()
    {
        type = EnemyType.Nomal;

        base.ChildStart();
    }

    protected override void Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * scoutSpeed);    // 아래로 이동

        base.Move();
    }

    protected override void Attack()
    {
        anim.SetTrigger("Attack");
        base.Attack();
    }

    protected override void ScoutAttack(GameObject collision)
    {
        if (collision.CompareTag("Player"))
        {
            StatesManager.instance.ChangeHP(scoutDamage);
            StatesManager.instance.ChangeMaterial();
            StatesManager.instance.Death();
            health = 0;
        }

        base.ScoutAttack(collision);
    }
}
