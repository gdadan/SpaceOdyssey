using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BulletSlot : MonoBehaviour
{
    //������ ������ ����
    public enum SlotType
    {
        Shop,
        Inventory,
        Equip,
        InnerShop
    }

    public SlotType slotType = SlotType.Inventory;

    public int key; //������ key��

    public string type;

    public List<Sprite> itemSprites; //������ ������ �̹���
    public Image itemImage;

    Button bulletBtn;

    private void Start()
    {
        bulletBtn = GetComponent<Button>();
        bulletBtn.onClick.AddListener(() => OnClickBulletBtn());

        itemImage.sprite = itemSprites[key - 1];
    }

    //������ Ŭ�� �� ���� ������ �κ��丮 �˾�â ���� �Լ�
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

    //�����۽��� ����
    public void SetSlot(int _key, SlotType _slotType)
    {
        key = _key;
        slotType = _slotType;
        itemImage.sprite = itemSprites[key - 1];
    }
}
