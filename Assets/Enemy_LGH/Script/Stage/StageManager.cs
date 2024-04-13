using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    // ���� �� �ִϸ��̼� ������ �� Prefab �ޱ�
    [Header("��������1")]           // 1�������� Enemy
    public Enemy Stage1Scout; 
    public Enemy Stage1Battlecruiser;
    public Enemy Stage1Dereadnought;
    public Enemy Stage1Fighter;
    public Enemy Stage1SupportShip;
    public Enemy Stage1Frigate;

    [Header("��������2")]          // 2�������� Enemy
    public Enemy Stage2Scout;
    public Enemy Stage2Battlecruiser;
    public Enemy Stage2Dereadnought;
    public Enemy Stage2Fighter;
    public Enemy Stage2SupportShip;
    public Enemy Stage2Frigate;

    [Header("��������3")]          // 3�������� Enemy
    public Enemy Stage3Scout;
    public Enemy Stage3Battlecruiser;
    public Enemy Stage3Dereadnought;
    public Enemy Stage3Fighter;
    public Enemy Stage3SupportShip;
    public Enemy Stage3Frigate;

    public bool stageEnd = true;                           // ���� ���������� �������� Ȯ���ϴ� bool ����
    
    public int stageNum = 0;                                // ���� �������� ���� index
    
    public List<Stage> stages = new List<Stage>();          // ��ü �������� List

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // ���������� ������ ���� ���������� Ȱ��ȭ�ϰ� ���������� ������ ���� ���·� �ٲ�
        if (stageEnd && stageNum < 15)
        {
            stages[stageNum].gameObject.SetActive(true);
            stageNum++;
            stageEnd = false;
        }
    }
}
