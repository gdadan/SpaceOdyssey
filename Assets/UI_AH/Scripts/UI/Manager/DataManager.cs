using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�ΰ��� ������
[Serializable]
public class InGameData
{
    public PlaneStatData ig_planeStatData;

    public BulletData ig_playerBulletData;
    public List<ItemData> ig_playerItemData = new List<ItemData>();
}

//����� ������
[Serializable]
public class PlaneStatData
{
    public int atk;
    public int def;
}

//���� ������
[Serializable]
public class PlayerData
{
    public int gold;
    public int gem;

    public float skillAtk;

    public List<int> userSkills = new List<int>();
    public List<BulletData> playerBulletData = new List<BulletData>();
    public List<ItemData> playerItemData = new List<ItemData>();
}

//���� �Ѿ� ������
[Serializable]
public class BulletData
{
    public int bulletKey;
    public int bulletCount;

    public BulletData (int _bulletKey, int _bulletCount)
    {
        this.bulletKey = _bulletKey;
        this.bulletCount = _bulletCount;
    }
}
//���� ������ ������
[Serializable]
public class ItemData
{
    public int itemKey;
    public int itemCount;

    public ItemData(int _itemKey, int _itemCount)
    {
        this.itemKey = _itemKey;
        this.itemCount = _itemCount;
    }
}

//���� ������ �����ϴ� ��ũ��Ʈ
public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData playerData;

    public InGameData inGameData;

    public PlaneStatData planeStatData;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        TestPlayerData();
    }

    //������ ������ �ҷ�����
    public void TestPlayerData()
    {
        playerData = new PlayerData();

        for (int i = 1; i < 13; i++)
        {
            playerData.playerBulletData.Add(new BulletData(i, 1));
        }
        for (int i = 0; i < 3; i++)
        {
            playerData.playerItemData.Add(new ItemData(i, 10));
        }

        for (int i = 0;i < 3; i++)
        {
            inGameData.ig_playerItemData.Add(new ItemData(i, 0));
        }

        inGameData.ig_playerBulletData = new BulletData(5, 1);

        playerData.gold = 50000;
        playerData.gem = 50000;
    }

    //�Ѿ� ����Ʈ ����
    public void RemoveBulletData(int key)
    {
        int index = playerData.playerBulletData.RemoveAll(o => o.bulletKey == key);
    }
}
