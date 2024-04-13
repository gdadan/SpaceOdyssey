using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_2 : Stage
{
    string stageName = "Stage 3-2";
    protected override void StageStart()
    {
        StartCoroutine(Stage3_2Start());
        base.StageStart();
    }

    IEnumerator Stage3_2Start()
    {
        StageName(stageName);
        yield return new WaitForSeconds(3f);

        // Scout 10�⸦ 2���� ���� ���� ��ġ�� ��ȯ
        // �÷��̾ ������ �ʰ� ������ ������
        for (int i = 0; i < 2; i++)
        {
            CreatEnemy(StageManager.instance.Stage3Scout, 10);
            yield return new WaitForSeconds(2f);
        }

        // Frigate 5�⸦ ���� ��ġ�� ��ȯ
        // �÷��̾ ���� �����̴� �÷��̾� ���� �� ����
        CreatEnemy(StageManager.instance.Stage3Frigate, 5);

        // Scout 10�⸦ 3���� ���� ���� ��ġ�� ��ȯ
        // �÷��̾ ������ �ʰ� ������ ������
        for (int i = 0; i < 3; i++)
        {
            CreatEnemy(StageManager.instance.Stage3Scout, 10);
            yield return new WaitForSeconds(2f);
        }

        StartCoroutine(CheckClear());
    }
}
