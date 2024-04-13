using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;               // Enemy 현재 체력
    [SerializeField] protected int maxHealth;      // Enemy 최대 체력
    protected float damage;               // Enemy 데미지
    [SerializeField] float deadTime = 0.8f;

    public bool lookPlayerOn = false;

    public enum State : int { Move, Attack, Dead, Broken } // Enemy 현재 상태
    public enum EnemyType : int { Nomal, Boss };

    public State state = State.Move;
    public EnemyType type;

    [Header("골드 값 및 골드 위치 값")]
    [SerializeField] protected int goldCount;      // Gold 생성 수
    [SerializeField] protected int goldNum;        // Gold 양
    [SerializeField] protected float plusX;          // Gold 생성 위치 X
    [SerializeField] protected float plusY;          // Gold 생성 위치 Y

    public Material flashWhite;
    Material currentMaterial;

    protected Animator anim;            // Enemy Animator
    protected SpriteRenderer spriteRenderer;


    private void Start()
    {
        anim = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();                             // 현재 체력을 최대 체력으로
        currentMaterial = spriteRenderer.material;

        health = maxHealth;                             // 현재 체력을 최대 체력으로
        StartCoroutine(CheckDead());                    // 현재 체력이 0이하인지 체크(죽었는지 체크)
        ChildStart();
    }

    private void Update()
    {
        switch (state)
        {
            case State.Move:    // Move 상태일 시 행동
                Move();
                break;

            case State.Attack:  // Attack 상태일 시 행동
                Attack();
                break;

            case State.Dead:    // Dead 상태일 시 행동
                StartCoroutine(Dead());
                state = State.Broken;
                break;

            case State.Broken:
                break;
        }

        // 맵에서 벗어날 시 다시 나타남
        Respawn();
    }

    protected virtual void Move()
    {
        // 유닛의 이동패턴
    }

    protected virtual void Attack()
    {
        // 각 유닛의 공격패턴
    }

    protected virtual void ChildStart()
    {
        // 자식들에서 Start에 넣을 함수들
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
        // 스카우트 공격
    }

    // 죽음상태시 오브젝트 비활성화 및 현재 스테이지 Enemy List에서 제거
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
    // 체력이 0이하로 내려가면 죽음상태로 변경
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

    // 적이 플레이어를 지나치면 다시 위에서 나오도록 조정 (Bullet에는 적용 안됨)
    // Scout밖에 적용되지 않을 듯 하다
    // 나머지 적들은 플레이이를 지나칠 일이 없을 것 같다
    void Respawn()
    {
        if (transform.position.y < -6)
        {
            Vector3 respawnPos = ((Vector3.right) + (Vector3.up * 12));
            transform.position += respawnPos;
            LookPlayer();
        }
    }

    // Gold 생성
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

    // Enemy가 Player를 보도록 한다
    public void LookPlayer()
    {
        float difX = GameManager.instance.player.transform.position.x - transform.position.x;         // Enemy와 Player의 x좌표 차이
        float difY = GameManager.instance.player.transform.position.y - transform.position.y;         // Enemy와 Player의 y좌표 차이

        float radAngel = Mathf.Atan2(difY, difX);       // Enemy와 Player의 각도(radian)

        // Euler 각으로 Enemy z축 회전 조절(플레이어를 향해)
        // Enemy Prefab이 기본적으로 아래를 보고있기 때문에 90도를 보정해줌
        transform.rotation = Quaternion.Euler(0, 0, radAngel * 180f / Mathf.PI + 90);
    }

    // Enemy가 Player를 보도록 한다
    protected IEnumerator LookPlayerOn()
    {
        while (true)
        {
            if (lookPlayerOn)
            {
                float difX = GameManager.instance.player.transform.position.x - transform.position.x;         // Enemy와 Player의 x좌표 차이
                float difY = GameManager.instance.player.transform.position.y - transform.position.y;         // Enemy와 Player의 y좌표 차이

                float radAngel = Mathf.Atan2(difY, difX);       // Enemy와 Player의 각도(radian)

                // Euler 각으로 Enemy z축 회전 조절(플레이어를 향해)
                // Enemy Prefab이 기본적으로 아래를 보고있기 때문에 90도를 보정해줌
                transform.rotation = Quaternion.Euler(0, 0, radAngel * 180f / Mathf.PI + 90);
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
}
