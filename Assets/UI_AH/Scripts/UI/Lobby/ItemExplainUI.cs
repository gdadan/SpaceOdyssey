using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemExplainUI : MonoBehaviour
{
    public BulletSlot bulletSlot;

    public GameObject itemPopUp; //아이템 팝업창
    public GameObject equipBtn; //아이템 장착 버튼
    public GameObject buyBtn; //아이템 구매 버튼
    public GameObject innerBuyBtn;
    public GameObject removeBtn; //아이템 헤제 버튼
    //public GameObject playerImage; //장비창 플레이어 이미지

    public TextMeshProUGUI itemName; //아이템 이름
    public TextMeshProUGUI itemSynergy1; //아이템 시너지1
    public TextMeshProUGUI itemSynergy2; //아이템 시너지2
    public TextMeshProUGUI itemSynergy3; //아이템 시너지3
    public TextMeshProUGUI itemAtk; //아이템 공격력
    public TextMeshProUGUI itemDef; //아이템 방어력
    public TextMeshProUGUI itemPrice; //아이템 가격
    public TextMeshProUGUI innerItemPrice;

    public Image popupItemImage; //팝업창 아이템 이미지

    public int cusItemKey;

    //아이템 팝업창 Closed
    public void CloseItemPopUp()
    {
        SoundManager.instance.PlaySFX(0);

        itemPopUp.SetActive(false);
        //playerImage.SetActive(true);
        buyBtn.SetActive(false);
        innerBuyBtn.SetActive(false);
        equipBtn.SetActive(false);
        removeBtn.SetActive(false);
    }

    //아이템 설명
    public void ItemExplain(int key)
    {
        cusItemKey = key;
        popupItemImage.sprite = bulletSlot.itemSprites[cusItemKey - 1];
        itemName.text = TsvLoader.instance.GetString(cusItemKey, "name");
        itemSynergy1.text = TsvLoader.instance.GetString(cusItemKey, "synergy1");
        itemSynergy2.text = TsvLoader.instance.GetString(cusItemKey, "synergy2");
        itemSynergy3.text = TsvLoader.instance.GetString(cusItemKey, "synergy3");
        itemAtk.text = "공격력 : " + TsvLoader.instance.GetInt(cusItemKey, "atk").ToString();
        itemDef.text = "방어력 : " + TsvLoader.instance.GetInt(cusItemKey, "def").ToString();
        itemPrice.text = TsvLoader.instance.GetInt(cusItemKey, "price").ToString();
        innerItemPrice.text = TsvLoader.instance.GetInt(cusItemKey, "innerPrice").ToString();
    }
    
    //상태에 따라 필요한 버튼(구매, 장착, 해제) 활성화
    public void ButtonActive(BulletSlot.SlotType slotType)
    {
        switch (slotType)
        {
            case BulletSlot.SlotType.Shop:
                buyBtn.SetActive(true);
                break;
            case BulletSlot.SlotType.Inventory:
                equipBtn.SetActive(true);
                break;
            case BulletSlot.SlotType.InnerShop:
                innerBuyBtn.SetActive(true);
                break;
            //case BulletSlot.SlotType.Equip:
            //    removeBtn.SetActive(true);
            //    break;
        }
    }
}
