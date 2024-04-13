using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Stage1_3 : Stage
{
    string stageName = "Stage 1-3";

    protected override void StageStart()
    {
        marketStage = true;
        StartCoroutine(Stage1_3Start());

        base.StageStart();
    }

    IEnumerator Stage1_3Start()
    {
        StageName(stageName);
        // Fighter 10기를 랜덤 위치에 소환
        // 밑으로 내려오다 y좌표가 3일 때 부터 옆으로 이동을 반복하며 공격
        CreatEnemy(StageManager.instance.Stage1Fighter, 10);

        // Frigate 5기를 랜덤 위치에 소환
        CreatEnemy(StageManager.instance.Stage1Frigate, 5);

        yield return new WaitForSeconds(2f);

        StartCoroutine(CheckClear());
    }
}
