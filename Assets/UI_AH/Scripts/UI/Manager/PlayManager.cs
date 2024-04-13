using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;

    public GameObject playPopUp; //플레이 팝업창
    //public GameObject playerImage;

    public List<ItemSlot> itemSlots; //유저가 가지고 있는 아이템들
    public List <ItemSlot> equipSlots; //유저가 게임에 가지고 갈 장착 아이템들

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
        //처음 아이템 세팅
        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].SetSlot(itemSlots[i].itemKey, ItemSlot.SlotType.Inventory);
            itemSlots[i].UpdateItemCount(0, 0);
            DataManager.instance.inGameData.ig_playerItemData[i].itemCount = 0;
        }
    }
    //게임플레이 버튼 클릭 시 팝업창 Open
    public void OnClickGamePlayBtn()
    {
        SoundManager.instance.PlaySFX(0);

        //playerImage.SetActive(false);
        playPopUp.SetActive(true);
    }

    //팝업창 Close
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

    //팝업창 안의 GamePlay
    public void GamePlay()
    {
        SoundManager.instance.PlaySFX(0);

        //start 버튼을 누르면 로딩 화면에서 게임 플레이 화면으로
        LoadingSceneController.LoadScene("PlayScene");
    }

    //아이템 장착
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

    //아이템 해제
    public void RemoveItem(int order)
    {
        equipSlots[order].gameObject.SetActive(false);
    }
}
