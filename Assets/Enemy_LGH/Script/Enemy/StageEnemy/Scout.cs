using UnityEngine;

public class Scout : Enemy
{
    // Scout �̵� ���
    // �Ʒ��� �̵�(�÷��̾� �������� �̵��� ���� �ְ� �ƴ� ���� �ִ�)
    // �÷��̾�� �̵��� ������ Enemy�� LookPlayer�� �̿��Ͽ� �̵��Ѵ�.

    // Scout ���� ���
    // �÷��̾�� �ε����� �������� �ش�.
    // �� �� �ٸ� ���� ����� ����.

    [SerializeField] float scoutSpeed = 5f;          // Scout ���ǵ�
    [SerializeField] float scoutDamage = 10f;        // Scout ������

    protected override void ChildStart()
    {
        type = EnemyType.Nomal;

        base.ChildStart();
    }

    protected override void Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * scoutSpeed);    // �Ʒ��� �̵�

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
