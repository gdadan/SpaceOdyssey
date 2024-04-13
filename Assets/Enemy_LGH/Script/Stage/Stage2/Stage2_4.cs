using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_4 : Stage
{
    string stageName = "Stage 2-4";
    protected override void StageStart()
    {
        StartCoroutine(Stage2_4Start());

        base.StageStart();
    }

    IEnumerator Stage2_4Start()
    {
        StageName(stageName);
        // Fighter 10기를 랜덤 위치에 소환
        // 밑으로 내려오다 y좌표가 3일 때 부터 옆으로 이동을 반복하며 공격
        CreatEnemy(StageManager.instance.Stage2Fighter, 10);
        yield return new WaitForSeconds(2f);

        // Frigate 5기를 랜덤 위치에 소환
        CreatEnemy(StageManager.instance.Stage2Frigate, 5);
        yield return new WaitForSeconds(2f);

        CreatEnemy(StageManager.instance.Stage2Scout, 10);
        yield return new WaitForSeconds(2f);

        // Frigate 5기를 랜덤 위치에 소환
        CreatEnemy(StageManager.instance.Stage2Frigate, 5);

        StartCoroutine(CheckClear());
    }
}
