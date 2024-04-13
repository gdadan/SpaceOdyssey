using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BulletSlot : MonoBehaviour
{
    //아이템 각각의 상태
    public enum SlotType
    {
        Shop,
        Inventory,
        Equip,
        InnerShop
    }

    public SlotType slotType = SlotType.Inventory;

    public int key; //아이템 key값

    public string type;

    public List<Sprite> itemSprites; //아이템 각각의 이미지
    public Image itemImage;

    Button bulletBtn;

    private void Start()
    {
        bulletBtn = GetComponent<Button>();
        bulletBtn.onClick.AddListener(() => OnClickBulletBtn());

        itemImage.sprite = itemSprites[key - 1];
    }

    //아이템 클릭 시 각각 상점과 인벤토리 팝업창 여는 함수
    public void OnClickBulletBtn()
    {
        SoundManager.instance.PlaySFX(0);

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            InnerShopManager.instance.OpenBulletPopUp(this);
        }
        else if (NestedScrollManager.instance.targetIndex == 0 && SceneManager.GetActiveScene().buildIndex == 2)
        {
            ShopManager.instance.OpenBulletPopUp(this);
        }
        else if (NestedScrollManager.instance.targetIndex == 1 && SceneManager.GetActiveScene().buildIndex == 2)
        {
            InventoryManager.instance.OpenBulletPopUp(this);
        }
    }

    //아이템슬롯 세팅
    public void SetSlot(int _key, SlotType _slotType)
    {
        key = _key;
        slotType = _slotType;
        itemImage.sprite = itemSprites[key - 1];
    }
}
