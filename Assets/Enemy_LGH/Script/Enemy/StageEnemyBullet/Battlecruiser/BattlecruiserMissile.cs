using System.Collections;
using UnityEngine;

public class BattlecruiserMissile : EnemyBullet
{
    float launchTime = 0f;

    float missileHp = 10f;

    public Material flashWhite;

    SpriteRenderer spriteRenderer;
    Material currentMaterial;

    [SerializeField] int modiDegree;

    Vector3 moveVec;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentMaterial = spriteRenderer.material;
    }

    private void OnEnable()
    {
        SoundManager.instance.PlaySFX(4);
    }

    protected override void Move()
    {
        launchTime += Time.deltaTime;
        moveVec = Vector3.left * enemyBulletSpeed;
        LookPlayer(modiDegree);
        transform.Translate(moveVec * Time.deltaTime);

        if (missileHp <= 0f)
        {
            gameObject.SetActive(false);
        }

        base.Move();
    }

    IEnumerator Hit()
    {
        spriteRenderer.material = flashWhite;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.material = currentMaterial;
    }

    protected override void Missile(GameObject collision)
    {
        if (!gameObject.activeSelf) return;
        if (collision.CompareTag("Bullet"))
        {
            missileHp -= collision.GetComponent<Bullet>().finalAtk;
            StartCoroutine(Hit());
            
        }

        base.Missile(collision);
    }
}
