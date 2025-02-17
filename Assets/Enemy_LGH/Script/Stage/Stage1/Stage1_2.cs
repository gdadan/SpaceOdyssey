using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Stage1_2 : Stage
{
    string stageName = "Stage 1-2";
    protected override void StageStart()
    {
        StartCoroutine(Stage1_2Start());
        base.StageStart();
    }

    IEnumerator Stage1_2Start()
    {
        StageName(stageName);

        // Scout 10기를 2번에 걸쳐 랜덤 위치에 소환
        // 플레이어를 향하지 않고 밑으로 내려감
        for (int i = 0; i < 2; i++)
        {
            CreatEnemy(StageManager.instance.Stage1Scout, 10);   
            yield return new WaitForSeconds(2f);
        }

        // Frigate 5기를 랜덤 위치에 소환
        // 플레이어를 향해 움직이다 플레이어 감지 시 공격
        CreatEnemy(StageManager.instance.Stage1Frigate, 5);

        // Scout 10기를 3번에 걸쳐 랜덤 위치에 소환
        // 플레이어를 향하지 않고 밑으로 내려감
        for (int i = 0; i < 3; i++)
        {
            CreatEnemy(StageManager.instance.Stage1Scout, 10);
            yield return new WaitForSeconds(2f);
        }
        StartCoroutine(CheckClear());
    }
}
