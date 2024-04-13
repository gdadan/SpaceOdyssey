using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlecruiser : Enemy
{
    protected float radialTime;
    protected float circleTime;
    protected float laserTime;
    protected float missileCoolTime;

    float endBoundary = 2f;

    Vector3 attackMoveVec;

    protected bool radialOn;
    protected bool circleOn;
    protected bool laserOn;

    public List<GameObject> missileLaunchers = new List<GameObject>();
    public List<GameObject> lasers = new List<GameObject>();

    protected override void ChildStart()
    {
        type = EnemyType.Boss;
        //attackMoveVec = Vector3.right * Time.deltaTime;
        StartCoroutine(MissileLauncher());
        StartCoroutine(LookPlayerOn());
        BattlecruiserStart();

        base.ChildStart();
    }

    protected override void Move()
    {
        transform.position += Vector3.down * Time.deltaTime;
        if (transform.position.y < 3.6f)
        {
            state = State.Attack;
        }
        base.Move();
    }

    protected override void Attack()
    {
        //AttackMove();

        BattlecruiserAttack();

        base.Attack();
    }

    protected virtual void BattlecruiserAttack()
    {
        // 海撇农风历 傍拜
    }

    protected virtual void BattlecruiserStart()
    {
        // 海撇农风历 start
    }

    void AttackMove()
    {
        transform.position += attackMoveVec;

        if (transform.position.x < -endBoundary) attackMoveVec = Vector3.right * Time.deltaTime;
        else if (transform.position.x > endBoundary) attackMoveVec = Vector3.left * Time.deltaTime;
    }

    IEnumerator MissileLauncher()
    {
        int port;

        while (true)
        {
            if (missileLaunchers.Count == 0) break;

            port = Random.Range(0, missileLaunchers.Count);

            missileLaunchers[port].SetActive(true);
            
            yield return new WaitForSeconds(missileCoolTime);
        }
    }

    
}
