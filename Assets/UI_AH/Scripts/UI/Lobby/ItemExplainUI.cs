using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemExplainUI : MonoBehaviour
{
    public BulletSlot bulletSlot;

    public GameObject itemPopUp; //������ �˾�â
    public GameObject equipBtn; //������ ���� ��ư
    public GameObject buyBtn; //������ ���� ��ư
    public GameObject innerBuyBtn;
    public GameObject removeBtn; //������ ���� ��ư
    //public GameObject playerImage; //���â �÷��̾� �̹���

    public TextMeshProUGUI itemName; //������ �̸�
    public TextMeshProUGUI itemSynergy1; //������ �ó���1
    public TextMeshProUGUI itemSynergy2; //������ �ó���2
    public TextMeshProUGUI itemSynergy3; //������ �ó���3
    public TextMeshProUGUI itemAtk; //������ ���ݷ�
    public TextMeshProUGUI itemDef; //������ ����
    public TextMeshProUGUI itemPrice; //������ ����
    public TextMeshProUGUI innerItemPrice;

    public Image popupItemImage; //�˾�â ������ �̹���

    public int cusItemKey;

    //������ �˾�â Closed
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

    //������ ����
    public void ItemExplain(int key)
    {
        cusItemKey = key;
        popupItemImage.sprite = bulletSlot.itemSprites[cusItemKey - 1];
        itemName.text = TsvLoader.instance.GetString(cusItemKey, "name");
        itemSynergy1.text = TsvLoader.instance.GetString(cusItemKey, "synergy1");
        itemSynergy2.text = TsvLoader.instance.GetString(cusItemKey, "synergy2");
        itemSynergy3.text = TsvLoader.instance.GetString(cusItemKey, "synergy3");
        itemAtk.text = "���ݷ� : " + TsvLoader.instance.GetInt(cusItemKey, "atk").ToString();
        itemDef.text = "���� : " + TsvLoader.instance.GetInt(cusItemKey, "def").ToString();
        itemPrice.text = TsvLoader.instance.GetInt(cusItemKey, "price").ToString();
        innerItemPrice.text = TsvLoader.instance.GetInt(cusItemKey, "innerPrice").ToString();
    }
    
    //���¿� ���� �ʿ��� ��ư(����, ����, ����) Ȱ��ȭ
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
