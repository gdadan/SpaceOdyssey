using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventoryManager : BulletPopupManager
{
    public static InventoryManager instance;

    public BulletSlot equipSlot; //장착 아이템 슬롯

    public List<BulletSlot> bulletSlots = new List<BulletSlot>(); //유저가 가지고 있는 인벤토리 아이템들

    public Transform inventoryTr; //인벤토리 슬롯 공간
    public Transform equipSlotTr; //인벤토리 장착 슬롯 공간

    public GameObject itemPrefab; //아이템 프리팹

    public TextMeshProUGUI atkText; //공격력 텍스트
    public TextMeshProUGUI defText; //방어력 텍스트
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI gemText;

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

        itemExplainUI.equipBtn.GetComponent<Button>().onClick.AddListener(() => OnClickEquipBtn());
        itemExplainUI.removeBtn.GetComponent<Button>().onClick.AddListener( () => OnClickRemoveBtn());
    }
    private void Start()
    {
        LoadInventoryData();
        UpdateGold(0);
        UpdateGem(0);

        //처음 비행기 배치
        equipSlot.key = DataManager.instance.inGameData.ig_playerBulletData.bulletKey;
        equipSlot.SetSlot(equipSlot.key, BulletSlot.SlotType.Equip);
        DataManager.instance.planeStatData.atk = TsvLoader.instance.GetInt(equipSlot.key, "atk");
        DataManager.instance.planeStatData.def = TsvLoader.instance.GetInt(equipSlot.key, "def");
        UpdateStat(equipSlot.key, 0);
        DataManager.instance.inGameData.ig_playerBulletData = new BulletData(equipSlot.key, 1);
    }

    public override void OpenBulletPopUp(BulletSlot bulletSlot)
    {
        base.OpenBulletPopUp(bulletSlot);
        //itemExplainUI.playerImage.SetActive(false);
    }

    //장착 버튼 클릭 시 아이템이 장착되는 함수
    public void OnClickEquipBtn()
    {
        if (equipSlot != null)
        {
            OnClickRemoveBtn();
        }

        equipSlot = bulletSlot;
        itemExplainUI.CloseItemPopUp(); //아이템 팝업창 닫기
        bulletSlot.transform.SetParent(equipSlotTr); //인벤토리창에 있는 슬롯을 장비창으로
        bulletSlot.slotType = BulletSlot.SlotType.Equip; //슬롯타입 Equip
        UpdateStat(equipSlot.key, 1);
        DataManager.instance.inGameData.ig_playerBulletData = new BulletData(equipSlot.key, 1);

        DataManager.instance.RemoveBulletData(equipSlot.key);
        //equipSlot.SetSlot(cusItemKey, ItemSlot.SlotType.Equip);
        //equipSlot.gameObject.SetActive(true);
    }

    //해제 버튼 클릭 시 아이템이 해제되는 함수
    public void OnClickRemoveBtn()
    {
        itemExplainUI.CloseItemPopUp(); 
        equipSlot.transform.SetParent(inventoryTr);
        equipSlot.slotType = BulletSlot.SlotType.Inventory;
        UpdateStat(equipSlot.key, -1);

        DataManager.instance.playerData.playerBulletData.Add(new BulletData(equipSlot.key, 1));

        equipSlot = null;
    }

    //유저의 아이템 데이터 불러오는 함수
    void LoadInventoryData()
    {
        for ( int i = 0; i< DataManager.instance.playerData.playerBulletData.Count; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, inventoryTr);
            BulletSlot newItemSlot = newItem.GetComponent<BulletSlot>();
            newItem.SetActive(true);
            newItemSlot.SetSlot(DataManager.instance.playerData.playerBulletData[i].bulletKey, BulletSlot.SlotType.Inventory);
            bulletSlots.Add(newItemSlot);
        }
    }

    //장비 장착, 해제시 스탯 변동
    public void UpdateStat(int key, int num)
    {
        DataManager.instance.planeStatData.atk += (num) * TsvLoader.instance.GetInt(key, "atk");
        DataManager.instance.planeStatData.def += (num) * TsvLoader.instance.GetInt(key, "def");
        atkText.text = "공격력 : " + DataManager.instance.planeStatData.atk.ToString();
        defText.text = "방어력 : " + DataManager.instance.planeStatData.def.ToString();

        DataManager.instance.inGameData.ig_planeStatData.atk = DataManager.instance.planeStatData.atk;
        DataManager.instance.inGameData.ig_planeStatData.def = DataManager.instance.planeStatData.def;
    }

    //골드 값 변동
    public void UpdateGold(int price)
    {
        DataManager.instance.playerData.gold -= price;
        goldText.text = DataManager.instance.playerData.gold.ToString();
    }

    //보석 값 변동
    public void UpdateGem(int price)
    {
        DataManager.instance.playerData.gem -= price;
        gemText.text = DataManager.instance.playerData.gem.ToString();
    }
}
