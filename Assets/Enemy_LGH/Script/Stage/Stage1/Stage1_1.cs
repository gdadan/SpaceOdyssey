using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_1 : Stage
{
    string stageName = "Stage 1-1";

    protected override void StageStart()
    {
        StartCoroutine(Stage1_1Start());
        base.StageStart();
    }

    IEnumerator Stage1_1Start()
    {
        StageName(stageName);
        for (int i = 0; i < 2; i++)
        {
            CreatEnemy(StageManager.instance.Stage1Scout, 10);
            yield return new WaitForSeconds(2f);
        }
        StartCoroutine(CheckClear());
    }
}
