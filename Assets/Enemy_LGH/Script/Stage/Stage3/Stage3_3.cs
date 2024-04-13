using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_3 : Stage
{
    string stageName = "Stage 3-3";
    protected override void StageStart()
    {
        marketStage = true;

        StartCoroutine(Stage3_3Start());

        base.StageStart();
    }

    IEnumerator Stage3_3Start()
    {
        StageName(stageName);
        // Fighter 10�⸦ ���� ��ġ�� ��ȯ
        // ������ �������� y��ǥ�� 3�� �� ���� ������ �̵��� �ݺ��ϸ� ����
        CreatEnemy(StageManager.instance.Stage3Fighter, 10);

        // Frigate 5�⸦ ���� ��ġ�� ��ȯ
        CreatEnemy(StageManager.instance.Stage3Frigate, 5);

        yield return new WaitForSeconds(2f);

        StartCoroutine(CheckClear());
    }
}
