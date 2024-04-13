using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Stage1_5 : Stage
{
    string stageName = "Stage 1 BOSS";
    protected override void StageStart()
    {
        bossStage = true;
        marketStage = true;

        StartCoroutine(Stage1_5Start());

        base.StageStart();
    }

    IEnumerator Stage1_5Start()
    {
        StageName(stageName);
        yield return null;
        // Battlecruiser 1기를 랜덤 위치에 소환
        CreatEnemy(StageManager.instance.Stage1Battlecruiser, 1, 0f, 7.8f);
        SoundManager.instance.BGMStop();
        SoundManager.instance.BGMStart(2);
        StartCoroutine(CheckClear());
    }
}
