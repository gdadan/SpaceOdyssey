using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_4 : Stage
{
    string stageName = "Stage 1-4";
    protected override void StageStart()
    {
        StartCoroutine(Stage1_4Start());

        base.StageStart();
    }

    IEnumerator Stage1_4Start()
    {
        StageName(stageName);
        // Fighter 10�⸦ ���� ��ġ�� ��ȯ
        // ������ �������� y��ǥ�� 3�� �� ���� ������ �̵��� �ݺ��ϸ� ����
        CreatEnemy(StageManager.instance.Stage1Fighter, 10);
        yield return new WaitForSeconds(2f);

        // Frigate 5�⸦ ���� ��ġ�� ��ȯ
        CreatEnemy(StageManager.instance.Stage1Frigate, 5);
        yield return new WaitForSeconds(2f);

        CreatEnemy(StageManager.instance.Stage1Scout, 10);
        yield return new WaitForSeconds(2f);

        // Frigate 5�⸦ ���� ��ġ�� ��ȯ
        CreatEnemy(StageManager.instance.Stage1Frigate, 5);

        StartCoroutine(CheckClear());
    }
}
