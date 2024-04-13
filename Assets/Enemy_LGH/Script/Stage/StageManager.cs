using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    // 패턴 및 애니메이션 정리된 적 Prefab 받기
    [Header("스테이지1")]           // 1스테이지 Enemy
    public Enemy Stage1Scout; 
    public Enemy Stage1Battlecruiser;
    public Enemy Stage1Dereadnought;
    public Enemy Stage1Fighter;
    public Enemy Stage1SupportShip;
    public Enemy Stage1Frigate;

    [Header("스테이지2")]          // 2스테이지 Enemy
    public Enemy Stage2Scout;
    public Enemy Stage2Battlecruiser;
    public Enemy Stage2Dereadnought;
    public Enemy Stage2Fighter;
    public Enemy Stage2SupportShip;
    public Enemy Stage2Frigate;

    [Header("스테이지3")]          // 3스테이지 Enemy
    public Enemy Stage3Scout;
    public Enemy Stage3Battlecruiser;
    public Enemy Stage3Dereadnought;
    public Enemy Stage3Fighter;
    public Enemy Stage3SupportShip;
    public Enemy Stage3Frigate;

    public bool stageEnd = true;                           // 현재 스테이지가 끝났지는 확인하는 bool 변수
    
    public int stageNum = 0;                                // 현재 스테이지 순서 index
    
    public List<Stage> stages = new List<Stage>();          // 전체 스테이지 List

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // 스테이지가 끝나면 다음 스테이지를 활성화하고 스테이지가 끝나지 않은 상태로 바꿈
        if (stageEnd && stageNum < 15)
        {
            stages[stageNum].gameObject.SetActive(true);
            stageNum++;
            stageEnd = false;
        }
    }
}
