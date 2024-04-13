using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_1 : Stage
{
    string stageName = "Stage 3-1";

    protected override void StageStart()
    {
        StartCoroutine(Stage3_1Start());

        base.StageStart();
    }

    IEnumerator Stage3_1Start()
    {
        StageName(stageName);
        for (int i = 0; i < 5; i++)
        {
            CreatEnemy(StageManager.instance.Stage3Scout, 10);
            yield return new WaitForSeconds(2f);
        }

        StartCoroutine(CheckClear());
    }
}
