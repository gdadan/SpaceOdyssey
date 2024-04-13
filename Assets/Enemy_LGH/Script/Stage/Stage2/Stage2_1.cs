using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_1 : Stage
{
    string stageName = "Stage 2-1";

    protected override void StageStart()
    {
        StartCoroutine(Stage2_1Start());

        base.StageStart();
    }

    IEnumerator Stage2_1Start()
    {
        StageName(stageName);
        for (int i = 0; i < 5; i++)
        {
            CreatEnemy(StageManager.instance.Stage2Scout, 10);
            yield return new WaitForSeconds(2f);
        }

        StartCoroutine(CheckClear());
    }
}
