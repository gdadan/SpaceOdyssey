using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InnerShopManager : BulletPopupManager
{
    public static InnerShopManager instance;

    public GameObject insufficientGold;
    public Engine2 engine2;
    public Engine1 engine1;
    public Engine1_1 engine1_1;

    public TextMeshProUGUI atkText; //공격력 텍스트
    public TextMeshProUGUI defText; //방어력 텍스트
    public TextMeshProUGUI refreshPriceText; //새로고침 가격 텍스트
    public TextMeshProUGUI ig_goldText;

    public List<BulletSlot> shopSlots; //상점에 있는 총알들
    public List<BulletSlot> equipSlots; //장착 슬롯들

    public int refreshPrice; //새로고침 가격

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
        itemExplainUI.innerBuyBtn.GetComponent<Button>().onClick.AddListener(() => OnClickBuyBtn());

        for (int i = 0; i < equipSlots.Count; i++)
        {
            //타입 이름을 비교해서 자리에 맞는 인벤토리창으로 총알 장착
            if (TsvLoader.instance.GetString(DataManager.instance.inGameData.ig_playerBulletData.bulletKey, "type") == equipSlots[i].type)
            {
                equipSlots[i].gameObject.SetActive(true);
                equipSlots[i].SetSlot(DataManager.instance.inGameData.ig_playerBulletData.bulletKey, BulletSlot.SlotType.Equip);
                break;
            }
        }
        UpdateStatIG(1, 0);

        //처음 상점에 총알 랜덤 생성
        RefreshShop();
    }

    //아이템 팝업창 Open
    public override void OpenBulletPopUp(BulletSlot bulletSlot)
    {
        insufficientGold.SetActive(false);

        base.OpenBulletPopUp(bulletSlot);
        //itemExplainUI.playerImage.SetActive(false);
    }

    void OnClickBuyBtn()
    {
        if (GoldManager.instance.playerGold >= TsvLoader.instance.GetInt(bulletSlot.key, "innerPrice"))
        {
            //플레이어 돈 감소
            GoldUdateIG(TsvLoader.instance.GetInt(bulletSlot.key, "innerPrice"));

            bulletSlot.gameObject.SetActive(false); //상점 아이템 비활성화

            //상점에 있는 아이템 인벤토리창으로
            for (int i = 0; i < equipSlots.Count; i++)
            {
                //타입 이름을 비교해서 자리에 맞는 인벤토리창으로 총알 장착
                if (TsvLoader.instance.GetString(bulletSlot.key, "type") == equipSlots[i].type)
                {
                    if (equipSlots[i].gameObject.activeSelf == false)
                    {
                        equipSlots[i].gameObject.SetActive(true);
                    }
                    else if (equipSlots[i].gameObject.activeSelf == true)
                    {
                        UpdateStatIG(equipSlots[i].key, -1);
                    }
                    equipSlots[i].SetSlot(bulletSlot.key, BulletSlot.SlotType.Equip);
                    UpdateStatIG(bulletSlot.key, 1);
                    break;
                }
            }
            BulletsManager.instance.Calculateinstance(cusItemKey);
            BulletsManager.instance.CalculateSynergy();
            SynergyManager.instance.Synergy();
            StatesManager.instance.Defence = StatesManager.instance.MaxDefence;
            StatesManager.instance.ChangeHP(0);
        }
        else
        {
            insufficientGold.SetActive(true);
        }

        itemExplainUI.CloseItemPopUp(); //아이템 팝업창 닫기
    }

    //장비 장착, 해제시 스탯 변동
    public void UpdateStatIG(int key, int num)
    {
        DataManager.instance.inGameData.ig_planeStatData.atk += (num) * TsvLoader.instance.GetInt(key, "atk");
        DataManager.instance.inGameData.ig_planeStatData.def += (num) * TsvLoader.instance.GetInt(key, "def");
        atkText.text = "공격력 : " + DataManager.instance.inGameData.ig_planeStatData.atk.ToString();
        defText.text = "방어력 : " + DataManager.instance.inGameData.ig_planeStatData.def.ToString();
    }

    //상점 새로고침
    public void RefreshBullet(bool isFree)
    {
        List<int> itemKey = new List<int>();

        //돈이 없으면 새로고침 못함
        if (GoldManager.instance.playerGold < refreshPrice && isFree == false)
            return;

        for (int i = 0; i < shopSlots.Count; i++)
        {
            while (true)
            {
                int rand = Random.Range(1, bulletSlot.itemSprites.Count + 1);

                if (!itemKey.Contains(rand))
                {
                    itemKey.Add(rand);
                    shopSlots[i].SetSlot(rand, BulletSlot.SlotType.InnerShop);
                    shopSlots[i].gameObject.SetActive(true);
                    break;
                }
            }
        }

        //돈 감소
        if (isFree == false)
        {
            SoundManager.instance.PlaySFX(0);

            GoldUdateIG(refreshPrice);
            refreshPrice = refreshPrice + 400;
        }
        refreshPriceText.text = refreshPrice.ToString();
    }

    public void CloseStore()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        StatesManager.instance.ChangeHP(0);
        SoundManager.instance.BGMStop();
        //SoundManager.instance.BGMStart(1);
    }

    public void RefreshShop()
    {
        refreshPrice = 200;
        RefreshBullet(true);
    }

    public void GoldUdateIG(int price)
    {
        GoldManager.instance.playerGold -= price;
        ig_goldText.text = string.Format("<sprite=0> {0}", GoldManager.instance.playerGold);
    }
}
