using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] protected float enemyBulletSpeed;          // 적 총알의 속도

    public float enemyBulletDamage;                            // 적 총알의 데미지

    private void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        // 총알의 움직임
        // 직선 이동, 유도 이동 등
    }


    // 플레이어와 충돌했을 때 플레이어에게 데미지
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
        // 미사일 피격구현
    } 

    // EnemyBullet이 Player를 향하도록 한다
    public void LookPlayer(int currentDegree)
    {
        float difX = GameManager.instance.player.transform.position.x - transform.position.x;         // Enemy와 Player의 x좌표 차이
        float difY = GameManager.instance.player.transform.position.y - transform.position.y;         // Enemy와 Player의 y좌표 차이

        float radAngel = Mathf.Atan2(difY, difX);       // Enemy와 Player의 각도(radian)

        // Euler 각으로 Enemy z축 회전 조절(플레이어를 향해)
        transform.rotation = Quaternion.Euler(0, 0, radAngel * 180f / Mathf.PI + currentDegree);
    }
}
