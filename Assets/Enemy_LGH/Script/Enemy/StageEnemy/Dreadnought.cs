using UnityEngine;

public class Dreadnought : Enemy
{
    public Enemy supportshipPrefab;
    public GameObject LaserBeam;

    float dreadnoughtSpeed = 1f;
    float dreadnoughtAttackTime = 10f;

    int supportshipNum = 10;

    protected override void Move()
    {
        transform.position = Vector3.down * Time.deltaTime * dreadnoughtSpeed;
        base.Move();
    }

    protected override void Attack()
    {
        dreadnoughtAttackTime += Time.deltaTime;
        if (health < maxHealth / 2)
        {
            SummonSupportship();
        }
        if (dreadnoughtAttackTime > 10f)
        {
            Laser();
            dreadnoughtAttackTime = 0f;
        }
        base.Attack();
    }

    void SummonSupportship()
    {
        for (int i = 0; i < supportshipNum; i++)
        {
            Enemy supportship = Instantiate<Enemy>(supportshipPrefab, transform);
            supportship.transform.position = transform.position;
        }
    }

    void Laser()
    {

    }
}