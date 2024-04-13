using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlecruiserStraightBullet : EnemyBullet
{
    protected override void Move()
    {
        transform.Translate(Vector3.right * Time.deltaTime * enemyBulletSpeed);

        base.Move();
    }
}
