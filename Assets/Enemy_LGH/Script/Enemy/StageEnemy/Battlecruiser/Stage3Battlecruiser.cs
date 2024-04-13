using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Battlecruiser : Battlecruiser
{
    protected override void BattlecruiserStart()
    {
        radialTime = 10f;
        circleTime = 10f;

        missileCoolTime = 0.5f;

        lookPlayerOn = false;

        base.BattlecruiserStart();
    }

    protected override void BattlecruiserAttack()
    {

        radialTime += Time.deltaTime;
        laserTime += Time.deltaTime;
        circleTime += Time.deltaTime;

        if (radialTime > 16)
        {
            radialOn = true;
            radialTime = 0;
        }

        if (laserTime > 14)
        {
            laserOn = true;
            laserTime = 0;
        }

        if (circleTime > 13)
        {
            circleOn = true;
            circleTime = 0;
        }
        /////////////////////////////////////////////////////
        if (radialOn)
        {
            transform.Find("RadialShooter").gameObject.SetActive(true);
            radialOn = false;
        }

        if (laserOn)
        {
            StartCoroutine(ShootLaser());
            laserOn = false;
        }

        if (circleOn)
        {
            transform.Find("CircleShotShooter").gameObject.SetActive(true);
            circleOn = false;
        }

        base.BattlecruiserAttack();
    }

    IEnumerator ShootLaser()
    {
        lasers[1].gameObject.SetActive(true);
        lasers[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);

        lasers[0].gameObject.SetActive(true);
    }
}
