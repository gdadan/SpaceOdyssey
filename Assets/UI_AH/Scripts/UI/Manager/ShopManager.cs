using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class ShopManager : BulletPopupManager
{
    public static ShopManager instance;

    public Scrollbar shopScrollbar; //���� ����ũ�ѹ�

    public TextMeshProUGUI moneyCountPopUpText; //�˾�â���� ��ȭ ���� �ؽ�Ʈ
    public TextMeshProUGUI moneyPricePopUpText; //�˾�â���� ��ȭ ���� �ؽ�Ʈ
    public TextMeshProUGUI itemPriceText; //�˾�â���� ������ ����
    public TextMeshProUGUI itemExplainText; //�˾�â���� ������ ����
    public TextMeshProUGUI itemCountText; //�˾�â���� ������ ���� ��

    public GameObject moneyPopUp; //��ȭ ���� �˾�â
    public GameObject itemPopUp; //������ ���� �˾�â
    public GameObject notEnoughGoldPopUp; //��� ���� �˾�â
    public GameObject notEnoughGemPopUp; //���� ���� �˾�â

    public Image moneyBuyBtnItemImage; //���Ź�ư���� ��ȭ �̹���
    public Image moneyPopUpItemImage; //�˾�â���� ��ȭ �̹���
    public Image itemImage; //�˾�â���� ������ �̹���

    public List<Sprite> moneyBuyItemList; //�˾�â���� ��ȭ �̹�����

    public List<BulletSlot> shopSlots;

    [HideInInspector]
    public MoneySlot moneySlot;
    [HideInInspector]
    public ItemSlot itemSlot;

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
        itemExplainUI.buyBtn.GetComponent<Button>().onClick.AddListener(() => OnClickBuyBtn());
    }

    private void Start()
    {
        RandomBullet();
    }

    public override void OpenBulletPopUp(BulletSlot bulletSlot)
    {
        base.OpenBulletPopUp(bulletSlot);
    }

    public void OpenItemPopUp(ItemSlot _itemSlot)
    {
        itemSlot = _itemSlot;
        itemPriceText.text = itemSlot.itemPrice.ToString();
        itemCountText.text = "������ : " + itemSlot.userItemSlot.itemCountText.text;
        itemImage.sprite = itemSlot.itemSprites[itemSlot.itemKey];
        if (itemSlot.itemKey == 0)
        {
            itemExplainText.text = "�������� ü���� ȸ���մϴ�.";
        }
        if (itemSlot.itemKey == 1)
        {
            itemExplainText.text = "���� �ð����� ��� ���� ����ݴϴ�.";
        }
        if (itemSlot.itemKey == 2)
        {
            itemExplainText.text = "���� �ð����� ���ظ� ���� �ʽ��ϴ�.";
        }
        itemPopUp.SetActive(true);
    }

    //���� ��ư Ŭ�� �� �Ѿ��� �κ��丮�� ���� �Լ�
    public void OnClickBuyBtn()
    {
        if (DataManager.instance.playerData.gold >= TsvLoader.instance.GetInt(cusItemKey, "price"))
        {
            bulletSlot.transform.SetParent(InventoryManager.instance.inventoryTr); //������ �ִ� ������ �κ��丮â����
            bulletSlot.slotType = BulletSlot.SlotType.Inventory; //����Ÿ�� ��ȯ

            DataManager.instance.playerData.playerBulletData.Add(new BulletData(cusItemKey, 1));

            InventoryManager.instance.UpdateGold(TsvLoader.instance.GetInt(cusItemKey, "price"));
        }
        else
        {
            OpenNotEnoughGoldPopUp();
        }

        itemExplainUI.CloseItemPopUp(); //������ �˾�â �ݱ�

        //itemSlot.gameObject.SetActive(false);
        //GameObject newItem = Instantiate(InventoryManager.instance.itemPrefab, InventoryManager.instance.inventoryTr);
        //newItem.SetActive(true);
        //newItem.GetComponent<ItemSlot>().SetSlot(cusItemKey, ItemSlot.SlotType.Inventory);
    }

    //Gold ��ư Ŭ�� �� ���� �ϴ� ������ �̵�
    public void OnClickGoldBtn(float value)
    {
        NestedScrollManager.instance.GoldClick();
        shopScrollbar.value = value;
    }

    //Gem ��ư Ŭ�� �� ���� �ϴ� ������ �̵�
    public void OnClickGemBtn()
    {
        OnClickGoldBtn(0);
    }

    //MoneyPopUp Close
    public void CloseMoneyPopUp()
    {
        SoundManager.instance.PlaySFX(0);

        moneyPopUp.SetActive(false);
    }

    //itemPopUp Close
    public void CloseItemPopUp()
    {
        SoundManager.instance.PlaySFX(0);

        itemPopUp.SetActive(false);
    }

    //��ȭ ���� �˾�â Close
    public void CloseNotEnoughGoldPopUp()
    {
        SoundManager.instance.PlaySFX(0);

        notEnoughGoldPopUp.SetActive(false);
    }
    public void CloseNotEnoughGemPopUp()
    {
        SoundManager.instance.PlaySFX(0);

        notEnoughGemPopUp.SetActive(false);
    }

    //��ȭ ���� �˾�â Open
    public void OpenNotEnoughGoldPopUp()
    {
        notEnoughGoldPopUp.SetActive(true);
    }

    void OpenNotEnoughGemPopUp()
    {
        notEnoughGemPopUp.SetActive(true);
    }

    //��ȭ ����
    public void OnClickMoneyBuyBtn()
    {
        if (moneySlot.moneyType == "Gold" && DataManager.instance.playerData.gem >= moneySlot.moneyPrice)
        {
            //���� ��� �� ����
            InventoryManager.instance.UpdateGold((-1) * moneySlot.moneyCount);
            //���� ���� �� ����
            InventoryManager.instance.UpdateGem(moneySlot.moneyPrice);
        }
        else if (moneySlot.moneyType == "Gem" && DataManager.instance.playerData.gold >= moneySlot.moneyPrice)
        {
            //���� ��� �� ����
            InventoryManager.instance.UpdateGold(moneySlot.moneyPrice);
            //���� ���� �� ����
            InventoryManager.instance.UpdateGem((-1) * moneySlot.moneyCount);
        }
        else
        {
            if (moneySlot.moneyType == "Gold")
            {
                OpenNotEnoughGemPopUp();
            }
            else
            {
                OpenNotEnoughGoldPopUp();
            }
        }

        CloseMoneyPopUp();
    }

    //������ ����
    public void OnClickItemBuyBtn()
    {
        if (DataManager.instance.playerData.gold >= itemSlot.itemPrice)
        {
            //���� ��� �� ����
            InventoryManager.instance.UpdateGold(itemSlot.itemPrice);
            //������ ���� �� ����
            itemSlot.userItemSlot.UpdateItemCount(1, 0);

            itemSlot.gameObject.SetActive(false);
        }
        else
        {
            OpenNotEnoughGoldPopUp();
        }

        CloseItemPopUp();
    }

    public void OnClickNotEnoughGoldPopUpBtn()
    {
        OnClickGoldBtn(0.41f);

        notEnoughGoldPopUp.SetActive(false);
    }
    public void OnClickNotEnoughGemPopUpBtn()
    {
        OnClickGemBtn();

        notEnoughGemPopUp.SetActive(false);
    }

    //��ȭ�� ���� �˾�â UI��ȭ 
    public void MoneyPopUpUI()
    {
        moneyCountPopUpText.text = moneySlot.moneyCount.ToString();
        moneyPricePopUpText.text = moneySlot.moneyPrice.ToString();
        moneyPopUpItemImage.sprite = moneySlot.moneyImage.sprite;
        if (moneySlot.moneyType == "Gold")
        {
            moneyBuyBtnItemImage.sprite = moneyBuyItemList[0];
        }
        else if (moneySlot.moneyType == "Gem")
        {
            moneyBuyBtnItemImage.sprite = moneyBuyItemList[1];
        }
    }

    public void RandomBullet()
    {
        List<int> itemKey = new List<int>();

        for (int i = 0; i < 14; i++)
        {
            itemKey.Add(i + 1);
        }

        for (int i = 0; i < DataManager.instance.playerData.playerBulletData.Count; i++)
        {
            itemKey.Remove(DataManager.instance.inGameData.ig_playerBulletData.bulletKey);
            itemKey.Remove(DataManager.instance.playerData.playerBulletData[i].bulletKey);
        }

        for (int i = 0; i < shopSlots.Count; i++)
        {
            if (itemKey.Count < 1)
            {
                return;
            }
            int rand = Random.Range(0, itemKey.Count);

            shopSlots[i].SetSlot(itemKey[rand], BulletSlot.SlotType.Shop);
            shopSlots[i].gameObject.SetActive(true);
            itemKey.Remove(itemKey[rand]);
        }
    }
}
