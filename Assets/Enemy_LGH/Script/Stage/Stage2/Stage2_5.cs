using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_5 : Stage
{
    string stageName = "Stage 2 BOSS";
    protected override void StageStart()
    {
        bossStage = true;
        marketStage = true;

        StartCoroutine(Stage2_5Start());

        base.StageStart();
    }

    IEnumerator Stage2_5Start()
    {
        StageName(stageName);
        yield return null;
        // Battlecruiser 1기를 랜덤 위치에 소환
        CreatEnemy(StageManager.instance.Stage2Battlecruiser, 1, 1.5f, 7.8f);
        CreatEnemy(StageManager.instance.Stage2Battlecruiser, 1, -1.5f, 7.8f);
        SoundManager.instance.BGMStop();
        SoundManager.instance.BGMStart(2);
        StartCoroutine(CheckClear());
    }
}
