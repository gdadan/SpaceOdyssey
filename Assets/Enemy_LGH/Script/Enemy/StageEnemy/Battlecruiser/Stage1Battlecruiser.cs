using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Battlecruiser : Battlecruiser
{
    protected override void BattlecruiserStart()
    {
        lookPlayerOn = false;

        radialTime = 10f;
        circleTime = 10f;

        base.BattlecruiserStart();
    }

    protected override void BattlecruiserAttack()
    {
        missileCoolTime = 0.8f;

        radialTime += Time.deltaTime;
        circleTime += Time.deltaTime;

        if (radialTime > 17)
        {
            radialOn = true;
            radialTime = 0;
        }

        if (circleTime > 13)
        {
            circleOn = true;
            circleTime = 0;
        }

        if (radialOn)
        {
            transform.Find("RadialShooter").gameObject.SetActive(true); 
            radialOn = false;
        }

        if (circleOn)
        {
            transform.Find("CircleShotShooter").gameObject.SetActive(true); 
            circleOn = false;
        }

        base.BattlecruiserAttack();
    }
}
