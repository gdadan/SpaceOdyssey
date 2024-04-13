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
        // Fighter 10�⸦ ���� ��ġ�� ��ȯ
        // ������ �������� y��ǥ�� 3�� �� ���� ������ �̵��� �ݺ��ϸ� ����
        CreatEnemy(StageManager.instance.Stage1Fighter, 10);

        // Frigate 5�⸦ ���� ��ġ�� ��ȯ
        CreatEnemy(StageManager.instance.Stage1Frigate, 5);

        yield return new WaitForSeconds(2f);

        StartCoroutine(CheckClear());
    }
}
