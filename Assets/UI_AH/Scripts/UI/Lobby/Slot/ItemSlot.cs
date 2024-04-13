using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    //������ ���� Ÿ��
    public enum SlotType
    {
        Inventory,
        Equip
    }

    public SlotType slotType = SlotType.Inventory;

    public List<Sprite> itemSprites; //������ ������ �̹���
    public Image itemImage;

    Button itemBtn;

    public int itemKey; //������ Ű��
    public int itemPrice; //������ ����
    public int order; //������ ����â ����

    public TextMeshProUGUI itemCountText;

    public ItemSlot userItemSlot; //�������� �� ����

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
