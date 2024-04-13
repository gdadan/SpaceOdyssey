using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_5 : Stage
{
    string stageName = "Stage 3 BOSS";

    protected override void StageStart()
    {
        bossStage = true;
        lastStage = true;

        StartCoroutine(Stage3_5Start());

        base.StageStart();
    }

    IEnumerator Stage3_5Start()
    {
        StageName(stageName);
        yield return null;
        // Battlecruiser 1기를 랜덤 위치에 소환
        CreatEnemy(StageManager.instance.Stage3Battlecruiser, 1, 0, 7.8f);
        SoundManager.instance.BGMStop();
        //SoundManager.instance.BGMStart(2);

        StartCoroutine(CheckClear());
    }
}
