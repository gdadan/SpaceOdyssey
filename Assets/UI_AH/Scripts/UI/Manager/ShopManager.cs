using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class ShopManager : BulletPopupManager
{
    public static ShopManager instance;

    public Scrollbar shopScrollbar; //상점 종스크롤바

    public TextMeshProUGUI moneyCountPopUpText; //팝업창에서 재화 수량 텍스트
    public TextMeshProUGUI moneyPricePopUpText; //팝업창에서 재화 가격 텍스트
    public TextMeshProUGUI itemPriceText; //팝업창에서 아이템 가격
    public TextMeshProUGUI itemExplainText; //팝업창에서 아이템 설명
    public TextMeshProUGUI itemCountText; //팝업창에서 아이템 소지 수

    public GameObject moneyPopUp; //재화 구매 팝업창
    public GameObject itemPopUp; //아이템 구매 팝업창
    public GameObject notEnoughGoldPopUp; //골드 부족 팝업창
    public GameObject notEnoughGemPopUp; //보석 부족 팝업창

    public Image moneyBuyBtnItemImage; //구매버튼에서 재화 이미지
    public Image moneyPopUpItemImage; //팝업창에서 재화 이미지
    public Image itemImage; //팝업창에서 아이템 이미지

    public List<Sprite> moneyBuyItemList; //팝업창에서 재화 이미지들

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
        itemCountText.text = "소지수 : " + itemSlot.userItemSlot.itemCountText.text;
        itemImage.sprite = itemSlot.itemSprites[itemSlot.itemKey];
        if (itemSlot.itemKey == 0)
        {
            itemExplainText.text = "일정량의 체력을 회복합니다.";
        }
        if (itemSlot.itemKey == 1)
        {
            itemExplainText.text = "일정 시간동안 모든 돈을 모아줍니다.";
        }
        if (itemSlot.itemKey == 2)
        {
            itemExplainText.text = "일정 시간동안 피해를 받지 않습니다.";
        }
        itemPopUp.SetActive(true);
    }

    //구매 버튼 클릭 시 총알이 인벤토리로 들어가는 함수
    public void OnClickBuyBtn()
    {
        if (DataManager.instance.playerData.gold >= TsvLoader.instance.GetInt(cusItemKey, "price"))
        {
            bulletSlot.transform.SetParent(InventoryManager.instance.inventoryTr); //상점에 있는 아이템 인벤토리창으로
            bulletSlot.slotType = BulletSlot.SlotType.Inventory; //슬롯타입 변환

            DataManager.instance.playerData.playerBulletData.Add(new BulletData(cusItemKey, 1));

            InventoryManager.instance.UpdateGold(TsvLoader.instance.GetInt(cusItemKey, "price"));
        }
        else
        {
            OpenNotEnoughGoldPopUp();
        }

        itemExplainUI.CloseItemPopUp(); //아이템 팝업창 닫기

        //itemSlot.gameObject.SetActive(false);
        //GameObject newItem = Instantiate(InventoryManager.instance.itemPrefab, InventoryManager.instance.inventoryTr);
        //newItem.SetActive(true);
        //newItem.GetComponent<ItemSlot>().SetSlot(cusItemKey, ItemSlot.SlotType.Inventory);
    }

    //Gold 버튼 클릭 시 구매 하는 곳으로 이동
    public void OnClickGoldBtn(float value)
    {
        NestedScrollManager.instance.GoldClick();
        shopScrollbar.value = value;
    }

    //Gem 버튼 클릭 시 구매 하는 곳으로 이동
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

    //재화 부족 팝업창 Close
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

    //재화 부족 팝업창 Open
    public void OpenNotEnoughGoldPopUp()
    {
        notEnoughGoldPopUp.SetActive(true);
    }

    void OpenNotEnoughGemPopUp()
    {
        notEnoughGemPopUp.SetActive(true);
    }

    //재화 구매
    public void OnClickMoneyBuyBtn()
    {
        if (moneySlot.moneyType == "Gold" && DataManager.instance.playerData.gem >= moneySlot.moneyPrice)
        {
            //소지 골드 수 증가
            InventoryManager.instance.UpdateGold((-1) * moneySlot.moneyCount);
            //소지 보석 수 감소
            InventoryManager.instance.UpdateGem(moneySlot.moneyPrice);
        }
        else if (moneySlot.moneyType == "Gem" && DataManager.instance.playerData.gold >= moneySlot.moneyPrice)
        {
            //소지 골드 수 감소
            InventoryManager.instance.UpdateGold(moneySlot.moneyPrice);
            //소지 보석 수 감소
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

    //아이템 구매
    public void OnClickItemBuyBtn()
    {
        if (DataManager.instance.playerData.gold >= itemSlot.itemPrice)
        {
            //소지 골드 수 감소
            InventoryManager.instance.UpdateGold(itemSlot.itemPrice);
            //아이템 소지 수 증가
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

    //재화에 따라 팝업창 UI변화 
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
