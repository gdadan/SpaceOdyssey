using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Battlecruiser : Battlecruiser
{
    protected override void BattlecruiserStart()
    {
        lookPlayerOn = true;
        missileCoolTime = 0.3f;
        base.BattlecruiserStart();
    }

    protected override void BattlecruiserAttack()
    {
        laserTime += Time.deltaTime;

        if (laserTime > 12)
        {
            laserOn = true;
            laserTime = 0;
        }

        if (laserOn) 
        {
            StartCoroutine(ShootLaser());
            laserOn = false; 
        }

        base.BattlecruiserAttack();
    }

    IEnumerator ShootLaser()
    {
        lasers[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        lookPlayerOn = false;

        yield return new WaitForSeconds(3f);
        lookPlayerOn = true;
    }
}
