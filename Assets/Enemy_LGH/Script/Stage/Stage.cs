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

    protected float maxSpawnX = 2.2f;               // ���� ������ ���� ��ġ ���� X
    protected float spawnPosY = 6f;                 // ���� ������ ���� ��ġ Y

    [Header("�� ���� ��ǥ")]
    float spawnPosX;                                // X ��ǥ �������� ������
    Vector2 spawnPos;                               // ���� �� ������ǥ

    [SerializeField] protected float stageTime;     // ���������� ����� �ð�
    [SerializeField] protected float stageMaxTime;  // ���������� �ִ�� ����� �ð� (�ð��� �������� �������� ����)

    protected bool marketStage = false;
    protected bool lastStage = false;
    protected bool bossStage = false;
    bool isErase = false;
    bool isClear = false;

    // Ȱ��ȭ �� �������� ����
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

    // �� �������� ����
    protected virtual void StageStart()
    {
        // �� ���������� ���� ����
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
                    goldText.text = string.Format("ȹ���� ��� <sprite=0> {0}", GoldManager.instance.playerGold);
                    SoundManager.instance.BGMStop();
                    SoundManager.instance.PlaySFX(1);
                    Time.timeScale = 0f;
                }
                StageEnd();
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    // �������� ���� (�����ð��� �����ų� ���� ��� óġ�ϸ� ȣ���Ͽ� �������� ����)
    // ���� �������� �ð����� ���������� ������ ����
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

    // ���� ������(Enemy�� ����, ������ Enemy�� ��)
    // ���� ���� X��ǥ�� �������� �����Ͽ� ���� ������ 
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

    // ������ ��ǥ�� �ƴ� ������ ��ǥ�� �� ����
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
