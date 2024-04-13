using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;

    public GameObject playPopUp; //�÷��� �˾�â
    //public GameObject playerImage;

    public List<ItemSlot> itemSlots; //������ ������ �ִ� �����۵�
    public List <ItemSlot> equipSlots; //������ ���ӿ� ������ �� ���� �����۵�

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

        SoundManager.instance.BGMStart(0);
    }

    private void Start()
    {
        //ó�� ������ ����
        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].SetSlot(itemSlots[i].itemKey, ItemSlot.SlotType.Inventory);
            itemSlots[i].UpdateItemCount(0, 0);
            DataManager.instance.inGameData.ig_playerItemData[i].itemCount = 0;
        }
    }
    //�����÷��� ��ư Ŭ�� �� �˾�â Open
    public void OnClickGamePlayBtn()
    {
        SoundManager.instance.PlaySFX(0);

        //playerImage.SetActive(false);
        playPopUp.SetActive(true);
    }

    //�˾�â Close
    public void ClosePopUp()
    {
        SoundManager.instance.PlaySFX(0);

        //playerImage.SetActive(true);
        playPopUp.SetActive(false);

        for (int i = 0;i < equipSlots.Count; i++)
        {
            if (equipSlots[i].gameObject.activeSelf == true)
            {
                RemoveItem(i);
                equipSlots[i].userItemSlot.UpdateItemCount(1, -1);
            }
        }
    }

    //�˾�â ���� GamePlay
    public void GamePlay()
    {
        SoundManager.instance.PlaySFX(0);

        //start ��ư�� ������ �ε� ȭ�鿡�� ���� �÷��� ȭ������
        LoadingSceneController.LoadScene("PlayScene");
    }

    //������ ����
    public void EquipItem(ItemSlot _itemSlot)
    {
        if (DataManager.instance.playerData.playerItemData[_itemSlot.itemKey].itemCount != 0)
        {
            for (int i = 0; i < equipSlots.Count; i++)
            {
                if (equipSlots[i].gameObject.activeSelf == false)
                {
                    equipSlots[i].userItemSlot = _itemSlot;
                    equipSlots[i].SetSlot(_itemSlot.itemKey, ItemSlot.SlotType.Equip);
                    equipSlots[i].gameObject.SetActive(true);
                    _itemSlot.UpdateItemCount(-1, 1);
                    break;
                }
            }
        }
    }

    //������ ����
    public void RemoveItem(int order)
    {
        equipSlots[order].gameObject.SetActive(false);
    }
}
