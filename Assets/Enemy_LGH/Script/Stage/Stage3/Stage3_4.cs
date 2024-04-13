using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_4 : Stage
{
    string stageName = "Stage 3-4";
    protected override void StageStart()
    {
        StartCoroutine(Stage3_4Start());

        base.StageStart();
    }

    IEnumerator Stage3_4Start()
    {
        StageName(stageName);
        // Fighter 10�⸦ ���� ��ġ�� ��ȯ
        // ������ �������� y��ǥ�� 3�� �� ���� ������ �̵��� �ݺ��ϸ� ����
        CreatEnemy(StageManager.instance.Stage3Fighter, 10);
        yield return new WaitForSeconds(2f);

        // Frigate 5�⸦ ���� ��ġ�� ��ȯ
        CreatEnemy(StageManager.instance.Stage3Frigate, 5);
        yield return new WaitForSeconds(2f);

        CreatEnemy(StageManager.instance.Stage3Scout, 10);
        yield return new WaitForSeconds(2f);

        // Frigate 5�⸦ ���� ��ġ�� ��ȯ
        CreatEnemy(StageManager.instance.Stage3Frigate, 5);

        StartCoroutine(CheckClear());
    }
}
