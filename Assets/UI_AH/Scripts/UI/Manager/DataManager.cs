using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//인게임 데이터
[Serializable]
public class InGameData
{
    public PlaneStatData ig_planeStatData;

    public BulletData ig_playerBulletData;
    public List<ItemData> ig_playerItemData = new List<ItemData>();
}

//비행기 데이터
[Serializable]
public class PlaneStatData
{
    public int atk;
    public int def;
}

//유저 데이터
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

//유저 총알 데이터
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
//유저 아이템 데이터
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

//유저 데이터 관리하는 스크립트
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

    //아이템 데이터 불러오기
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

    //총알 리스트 삭제
    public void RemoveBulletData(int key)
    {
        int index = playerData.playerBulletData.RemoveAll(o => o.bulletKey == key);
    }
}
