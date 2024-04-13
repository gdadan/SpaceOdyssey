using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;               // Enemy ���� ü��
    [SerializeField] protected int maxHealth;      // Enemy �ִ� ü��
    protected float damage;               // Enemy ������
    [SerializeField] float deadTime = 0.8f;

    public bool lookPlayerOn = false;

    public enum State : int { Move, Attack, Dead, Broken } // Enemy ���� ����
    public enum EnemyType : int { Nomal, Boss };

    public State state = State.Move;
    public EnemyType type;

    [Header("��� �� �� ��� ��ġ ��")]
    [SerializeField] protected int goldCount;      // Gold ���� ��
    [SerializeField] protected int goldNum;        // Gold ��
    [SerializeField] protected float plusX;          // Gold ���� ��ġ X
    [SerializeField] protected float plusY;          // Gold ���� ��ġ Y

    public Material flashWhite;
    Material currentMaterial;

    protected Animator anim;            // Enemy Animator
    protected SpriteRenderer spriteRenderer;


    private void Start()
    {
        anim = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();                             // ���� ü���� �ִ� ü������
        currentMaterial = spriteRenderer.material;

        health = maxHealth;                             // ���� ü���� �ִ� ü������
        StartCoroutine(CheckDead());                    // ���� ü���� 0�������� üũ(�׾����� üũ)
        ChildStart();
    }

    private void Update()
    {
        switch (state)
        {
            case State.Move:    // Move ������ �� �ൿ
                Move();
                break;

            case State.Attack:  // Attack ������ �� �ൿ
                Attack();
                break;

            case State.Dead:    // Dead ������ �� �ൿ
                StartCoroutine(Dead());
                state = State.Broken;
                break;

            case State.Broken:
                break;
        }

        // �ʿ��� ��� �� �ٽ� ��Ÿ��
        Respawn();
    }

    protected virtual void Move()
    {
        // ������ �̵�����
    }

    protected virtual void Attack()
    {
        // �� ������ ��������
    }

    protected virtual void ChildStart()
    {
        // �ڽĵ鿡�� Start�� ���� �Լ���
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Hit(collision.gameObject));
        ScoutAttack(collision.gameObject);
    }

    IEnumerator Hit(GameObject collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            spriteRenderer.material = flashWhite;
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.material = currentMaterial;
        }
    }

    protected virtual void ScoutAttack(GameObject collision)
    {
        // ��ī��Ʈ ����
    }

    // �������½� ������Ʈ ��Ȱ��ȭ �� ���� �������� Enemy List���� ����
    IEnumerator Dead()
    {
        anim.SetBool("isDead", true);
        SoundManager.instance.PlaySFX(8);

        if (type == EnemyType.Boss)
        {
            InstanceGold(goldCount, goldNum, plusX, plusY);
            BattlecruiserDead();
        }
        else InstanceGold(goldCount, goldNum);

        EnemyManager.instance.enemies.Remove(gameObject.GetComponent<Enemy>());

        yield return new WaitForSeconds(deadTime);
        gameObject.SetActive(false);
    }

    void BattlecruiserDead()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    // ü���� 0���Ϸ� �������� �������·� ����
    IEnumerator CheckDead()
    {
        while (true)
        {
            if (health <= 0 && !(state == State.Dead))
            {
                state = State.Dead;
                gameObject.GetComponent<Collider2D>().enabled = false;
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

    // ���� �÷��̾ ����ġ�� �ٽ� ������ �������� ���� (Bullet���� ���� �ȵ�)
    // Scout�ۿ� ������� ���� �� �ϴ�
    // ������ ������ �÷����̸� ����ĥ ���� ���� �� ����
    void Respawn()
    {
        if (transform.position.y < -6)
        {
            Vector3 respawnPos = ((Vector3.right) + (Vector3.up * 12));
            transform.position += respawnPos;
            LookPlayer();
        }
    }

    // Gold ����
    protected void InstanceGold(int goldCount, int goldNum)
    {
        for (int i = 0; i < goldCount; i++)
        {
            Gold gold = Instantiate(GoldManager.instance.goldPrefab, GoldManager.instance.gameObject.transform);
            gold.gold = goldNum;
            gold.transform.position = transform.position;
        }
    }

    protected void InstanceGold(int goldCount, int goldNum, float axisX, float axisY)
    {
        Vector3 goldPosition;
        for (int i = 0; i < goldCount; i++)
        {
            goldPosition = new Vector3(Random.Range(transform.position.x - axisX, transform.position.x + axisX), 
                Random.Range(transform.position.y - axisY, transform.position.y + axisY));
            Gold gold = Instantiate(GoldManager.instance.goldPrefab, GoldManager.instance.gameObject.transform);
            gold.gold = goldNum;
            gold.transform.position = goldPosition;
        }
    }

    // Enemy�� Player�� ������ �Ѵ�
    public void LookPlayer()
    {
        float difX = GameManager.instance.player.transform.position.x - transform.position.x;         // Enemy�� Player�� x��ǥ ����
        float difY = GameManager.instance.player.transform.position.y - transform.position.y;         // Enemy�� Player�� y��ǥ ����

        float radAngel = Mathf.Atan2(difY, difX);       // Enemy�� Player�� ����(radian)

        // Euler ������ Enemy z�� ȸ�� ����(�÷��̾ ����)
        // Enemy Prefab�� �⺻������ �Ʒ��� �����ֱ� ������ 90���� ��������
        transform.rotation = Quaternion.Euler(0, 0, radAngel * 180f / Mathf.PI + 90);
    }

    // Enemy�� Player�� ������ �Ѵ�
    protected IEnumerator LookPlayerOn()
    {
        while (true)
        {
            if (lookPlayerOn)
            {
                float difX = GameManager.instance.player.transform.position.x - transform.position.x;         // Enemy�� Player�� x��ǥ ����
                float difY = GameManager.instance.player.transform.position.y - transform.position.y;         // Enemy�� Player�� y��ǥ ����

                float radAngel = Mathf.Atan2(difY, difX);       // Enemy�� Player�� ����(radian)

                // Euler ������ Enemy z�� ȸ�� ����(�÷��̾ ����)
                // Enemy Prefab�� �⺻������ �Ʒ��� �����ֱ� ������ 90���� ��������
                transform.rotation = Quaternion.Euler(0, 0, radAngel * 180f / Mathf.PI + 90);
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
}
