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

        // Scout 10�⸦ 2���� ���� ���� ��ġ�� ��ȯ
        // �÷��̾ ������ �ʰ� ������ ������
        for (int i = 0; i < 2; i++)
        {
            CreatEnemy(StageManager.instance.Stage1Scout, 10);   
            yield return new WaitForSeconds(2f);
        }

        // Frigate 5�⸦ ���� ��ġ�� ��ȯ
        // �÷��̾ ���� �����̴� �÷��̾� ���� �� ����
        CreatEnemy(StageManager.instance.Stage1Frigate, 5);

        // Scout 10�⸦ 3���� ���� ���� ��ġ�� ��ȯ
        // �÷��̾ ������ �ʰ� ������ ������
        for (int i = 0; i < 3; i++)
        {
            CreatEnemy(StageManager.instance.Stage1Scout, 10);
            yield return new WaitForSeconds(2f);
        }
        StartCoroutine(CheckClear());
    }
}
