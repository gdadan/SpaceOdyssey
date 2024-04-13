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

    public BulletSlot equipSlot; //���� ������ ����

    public List<BulletSlot> bulletSlots = new List<BulletSlot>(); //������ ������ �ִ� �κ��丮 �����۵�

    public Transform inventoryTr; //�κ��丮 ���� ����
    public Transform equipSlotTr; //�κ��丮 ���� ���� ����

    public GameObject itemPrefab; //������ ������

    public TextMeshProUGUI atkText; //���ݷ� �ؽ�Ʈ
    public TextMeshProUGUI defText; //���� �ؽ�Ʈ
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

        //ó�� ����� ��ġ
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

    //���� ��ư Ŭ�� �� �������� �����Ǵ� �Լ�
    public void OnClickEquipBtn()
    {
        if (equipSlot != null)
        {
            OnClickRemoveBtn();
        }

        equipSlot = bulletSlot;
        itemExplainUI.CloseItemPopUp(); //������ �˾�â �ݱ�
        bulletSlot.transform.SetParent(equipSlotTr); //�κ��丮â�� �ִ� ������ ���â����
        bulletSlot.slotType = BulletSlot.SlotType.Equip; //����Ÿ�� Equip
        UpdateStat(equipSlot.key, 1);
        DataManager.instance.inGameData.ig_playerBulletData = new BulletData(equipSlot.key, 1);

        DataManager.instance.RemoveBulletData(equipSlot.key);
        //equipSlot.SetSlot(cusItemKey, ItemSlot.SlotType.Equip);
        //equipSlot.gameObject.SetActive(true);
    }

    //���� ��ư Ŭ�� �� �������� �����Ǵ� �Լ�
    public void OnClickRemoveBtn()
    {
        itemExplainUI.CloseItemPopUp(); 
        equipSlot.transform.SetParent(inventoryTr);
        equipSlot.slotType = BulletSlot.SlotType.Inventory;
        UpdateStat(equipSlot.key, -1);

        DataManager.instance.playerData.playerBulletData.Add(new BulletData(equipSlot.key, 1));

        equipSlot = null;
    }

    //������ ������ ������ �ҷ����� �Լ�
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

    //��� ����, ������ ���� ����
    public void UpdateStat(int key, int num)
    {
        DataManager.instance.planeStatData.atk += (num) * TsvLoader.instance.GetInt(key, "atk");
        DataManager.instance.planeStatData.def += (num) * TsvLoader.instance.GetInt(key, "def");
        atkText.text = "���ݷ� : " + DataManager.instance.planeStatData.atk.ToString();
        defText.text = "���� : " + DataManager.instance.planeStatData.def.ToString();

        DataManager.instance.inGameData.ig_planeStatData.atk = DataManager.instance.planeStatData.atk;
        DataManager.instance.inGameData.ig_planeStatData.def = DataManager.instance.planeStatData.def;
    }

    //��� �� ����
    public void UpdateGold(int price)
    {
        DataManager.instance.playerData.gold -= price;
        goldText.text = DataManager.instance.playerData.gold.ToString();
    }

    //���� �� ����
    public void UpdateGem(int price)
    {
        DataManager.instance.playerData.gem -= price;
        gemText.text = DataManager.instance.playerData.gem.ToString();
    }
}
