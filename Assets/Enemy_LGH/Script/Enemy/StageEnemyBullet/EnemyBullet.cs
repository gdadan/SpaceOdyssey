using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] protected float enemyBulletSpeed;          // �� �Ѿ��� �ӵ�

    public float enemyBulletDamage;                            // �� �Ѿ��� ������

    private void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        // �Ѿ��� ������
        // ���� �̵�, ���� �̵� ��
    }


    // �÷��̾�� �浹���� �� �÷��̾�� ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StatesManager.instance.ChangeHP(enemyBulletDamage);
            StatesManager.instance.ChangeMaterial();
            StatesManager.instance.Death();
            gameObject.SetActive(false);
        }
        Missile(collision.gameObject);
    }

    protected virtual void Missile(GameObject collision)
    {
        // �̻��� �ǰݱ���
    } 

    // EnemyBullet�� Player�� ���ϵ��� �Ѵ�
    public void LookPlayer(int currentDegree)
    {
        float difX = GameManager.instance.player.transform.position.x - transform.position.x;         // Enemy�� Player�� x��ǥ ����
        float difY = GameManager.instance.player.transform.position.y - transform.position.y;         // Enemy�� Player�� y��ǥ ����

        float radAngel = Mathf.Atan2(difY, difX);       // Enemy�� Player�� ����(radian)

        // Euler ������ Enemy z�� ȸ�� ����(�÷��̾ ����)
        transform.rotation = Quaternion.Euler(0, 0, radAngel * 180f / Mathf.PI + currentDegree);
    }
}
