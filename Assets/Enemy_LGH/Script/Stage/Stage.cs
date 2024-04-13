using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage : MonoBehaviour
{
    public TextMeshProUGUI stageNameText;
    public TextMeshProUGUI stageTimerText;
    public TextMeshProUGUI goldText;

    public GameObject backgroundAsteroid, backgroundPlanet;
    public GameObject innerMarket;
    public GameObject win;
    public GameObject stageClear;

    protected float maxSpawnX = 2.2f;               // 적이 등장할 스폰 위치 범위 X
    protected float spawnPosY = 6f;                 // 적이 등장할 스폰 위치 Y

    [Header("적 생성 좌표")]
    float spawnPosX;                                // X 좌표 범위에서 랜덤값
    Vector2 spawnPos;                               // 최종 적 스폰좌표

    [SerializeField] protected float stageTime;     // 스테이지가 진행된 시간
    [SerializeField] protected float stageMaxTime;  // 스테이지가 최대로 진행될 시간 (시간이 지남으로 스테이지 종료)

    protected bool marketStage = false;
    protected bool lastStage = false;
    protected bool bossStage = false;
    bool isErase = false;
    bool isClear = false;

    // 활성화 시 스테이지 시작
    private void OnEnable()
    {
        StageStart();
        StartCoroutine(BackGroundAsteroid());
        StartCoroutine(End());
    }

    private void Update()
    {
        stageTime += Time.deltaTime;
        stageTimerText.text = (stageMaxTime - stageTime).ToString("F0");
        if (stageMaxTime - stageTime <= 0)
        {
            stageTimerText.text = 0.ToString();
        }
        if (stageMaxTime > 1000)
        {
            stageTimerText.text = "???".ToString();
        }
    }

    IEnumerator BackGroundAsteroid()
    {
        while (true)
        {
            int rand = Random.Range(0, 11);
            if (rand < 1)
            {
                spawnPosX = Random.Range(-maxSpawnX, maxSpawnX);
                spawnPos = new Vector2(spawnPosX, spawnPosY);

                Instantiate(backgroundPlanet, spawnPos, Quaternion.identity);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    protected void StageName(string name)
    {
        stageNameText.text = name;
    }

    // 각 스테이지 패턴
    protected virtual void StageStart()
    {
        // 각 스테이지의 패턴 구현
    }

    IEnumerator End()
    {
        while (true)
        {
            if (stageTime >= stageMaxTime || isClear)
            {
                isErase = true;
                EraseEnemy();
                stageClear.SetActive(true);

                if (bossStage) yield return new WaitForSeconds(8f);
                else yield return new WaitForSeconds(4f);

                StatesManager.instance.Defence = StatesManager.instance.MaxDefence;
                StatesManager.instance.ChangeHP(0);

                stageClear.SetActive(false);

                if (marketStage)
                {
                    innerMarket.SetActive(true);
                    InnerShopManager.instance.RefreshShop();
                    InnerShopManager.instance.GoldUdateIG(0);
                    SoundManager.instance.BGMStop();
                    SoundManager.instance.BGMStart(3);
                    Time.timeScale = 0f;
                }
                if (lastStage)
                {
                    win.SetActive(true);
                    goldText.text = string.Format("획득한 골드 <sprite=0> {0}", GoldManager.instance.playerGold);
                    SoundManager.instance.BGMStop();
                    SoundManager.instance.PlaySFX(1);
                    Time.timeScale = 0f;
                }
                StageEnd();
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    // 스테이지 종료 (일정시간이 지나거나 적을 모두 처치하면 호출하여 스테이지 종료)
    // 보스 전에서는 시간으로 스테이지가 끝나지 않음
    protected void StageEnd()
    {
        StageManager.instance.stageEnd = true;
        gameObject.SetActive(false);
    }

    void EraseEnemy()
    {
        if (isErase)
        {
            for (int i = 0; i < EnemyManager.instance.enemies.Count; i++)
            {
                EnemyManager.instance.enemies[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < EnemyBulletManager.instance.enemyBullets.Count; i++)
            {
                EnemyBulletManager.instance.enemyBullets[i].gameObject.SetActive(false);
            }
            EnemyManager.instance.enemies.Clear();
            EnemyBulletManager.instance.enemyBullets.Clear();
        }
        isErase = false;
    }

    protected IEnumerator CheckClear()
    {
        while (true)
        {
            isClear = true;
            for (int i = 0; i < EnemyManager.instance.enemies.Count; i++)
            {
                if (EnemyManager.instance.enemies[i].gameObject.activeSelf)
                {
                    isClear = false;
                    break;
                }
            }
            yield return new WaitForSecondsRealtime(0.001f);
        }
    }

    // 적을 생성함(Enemy의 종류, 생성할 Enemy의 수)
    // 범위 안의 X좌표를 랜덤으로 생성하여 적을 생성함 
    protected void CreatEnemy(Enemy enemyKind, int enemyNum)
    {
        for (int i = 0; i < enemyNum; i++)
        {
            spawnPosX = Random.Range(-maxSpawnX, maxSpawnX);
            spawnPos = new Vector2(spawnPosX, spawnPosY);

            Enemy enemy = Instantiate(enemyKind, spawnPos, Quaternion.identity);
            enemy.transform.SetParent(EnemyManager.instance.transform);

            EnemyManager.instance.enemies.Add(enemy);
        }
    }

    // 랜덤한 좌표가 아닌 정해진 좌표에 적 생성
    protected void CreatEnemy(Enemy enemyKind, int enemyNum, float xAxis, float yAxis)
    {
        for (int i = 0; i < enemyNum; i++)
        {
            spawnPos = new Vector2(xAxis, yAxis);

            Enemy enemy = Instantiate(enemyKind, spawnPos, Quaternion.identity);
            enemy.transform.SetParent(EnemyManager.instance.transform);

            EnemyManager.instance.enemies.Add(enemy);
        }
    }
}
