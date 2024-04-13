using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    //아이템 슬롯 타입
    public enum SlotType
    {
        Inventory,
        Equip
    }

    public SlotType slotType = SlotType.Inventory;

    public List<Sprite> itemSprites; //아이템 각각의 이미지
    public Image itemImage;

    Button itemBtn;

    public int itemKey; //아이템 키값
    public int itemPrice; //아이템 가격
    public int order; //아이템 장착창 순서

    public TextMeshProUGUI itemCountText;

    public ItemSlot userItemSlot; //장착했을 때 슬롯

    void Start()
    {
        itemBtn = GetComponent<Button>();
        itemBtn.onClick.AddListener(() => OnClickItemBtn());
    }

    void OnClickItemBtn()
    {
        SoundManager.instance.PlaySFX(0);

        if (NestedScrollManager.instance.targetIndex == 0)
        {
            ShopManager.instance.OpenItemPopUp(this);
        }
        else if (NestedScrollManager.instance.targetIndex == 2 && slotType == SlotType.Inventory)
        {
            PlayManager.instance.EquipItem(this);
        }
        else if (NestedScrollManager.instance.targetIndex == 2 && slotType == SlotType.Equip)
        {
            userItemSlot.UpdateItemCount(1, -1);
            PlayManager.instance.RemoveItem(order);
        }
    }

    public void SetSlot(int _key, SlotType _slotType)
    {
        itemKey = _key;
        slotType = _slotType;
        itemImage.sprite = itemSprites[itemKey];
    }

    public void UpdateItemCount(int count1, int count2)
    {
        DataManager.instance.playerData.playerItemData[itemKey].itemCount += count1;
        DataManager.instance.inGameData.ig_playerItemData[itemKey].itemCount += count2;
        itemCountText.text = DataManager.instance.playerData.playerItemData[itemKey].itemCount.ToString();
    }
}
