using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkilltreeManager : MonoBehaviour
{
    public static SkilltreeManager instance;

    public SkillSlot skillSlot;

    public List<SkillSlot> skillSlots;

    public GameObject skillPopUp;
    public GameObject prePopUp;

    public Button buyBtn;

    public Image image;

    public TextMeshProUGUI priceText;
    public TextMeshProUGUI explainText;

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
    }

    private void Start()
    {
        LoadSkill();
    }

    public void OpenItemPopUp(SkillSlot _skillSlot, bool istrue)
    {
        skillSlot = _skillSlot;
        priceText.text = skillSlot.price.ToString();
        image.sprite = skillSlot.skillSprite;
        if (skillSlot.type == "atk")
        {
            explainText.text = "공격력 증가!";
        }
        if (skillSlot.type == "def")
        {
            explainText.text = "방어력 증가!";
        }
        if (skillSlot.type == "skill")
        {
            explainText.text = "강력한 공격을 합니다!";
        }

        skillPopUp.SetActive(true);
        prePopUp.SetActive(false);
        buyBtn.gameObject.SetActive(istrue);
    }

    public void OnClickBuyBtn()
    {
        if (DataManager.instance.playerData.gold >= skillSlot.price)
        {
            if (!DataManager.instance.playerData.userSkills.Contains(4))
            {
                if (skillSlot.key == 11)
                {
                    prePopUp.SetActive(true);
                    ClosePopUp();
                    return;
                }
            }
               
            if (skillSlot.type == "atk")
            {
                DataManager.instance.playerData.skillAtk += 0.3f;
            }
            //연결된 다음 스킬들 해제
            skillSlot.UnlockedNextSkill();
            //스킬 슬롯 배운 상태로 변경
            skillSlot.slotType = SkillSlot.SlotType.Learn;
            //스킬 슬롯 이미지를 밝게 변경
            skillSlot.unlockImage.SetActive(false);
            //업그레이드 이미지 비활성화
            skillSlot.upgradeImage.SetActive(false);
            //소지 골드 수 감소
            InventoryManager.instance.UpdateGold(skillSlot.price);

            DataManager.instance.playerData.userSkills.Add(skillSlot.key);
        }
        else
        {
            ShopManager.instance.OpenNotEnoughGoldPopUp();
        }
        ClosePopUp();
    }

    public void ClosePopUp()
    {
        SoundManager.instance.PlaySFX(0);

        skillPopUp.SetActive(false);
    }

    void LoadSkill()
    {
        for (int i = 0; i < DataManager.instance.playerData.userSkills.Count; i++)
        {
            for (int j = 0; j < skillSlots.Count; j++)
            {
                if (DataManager.instance.playerData.userSkills[i] == skillSlots[j].key)
                {
                    //연결된 다음 스킬들 해제
                    skillSlots[j].UnlockedNextSkill();
                    //스킬 슬롯 배운 상태로 변경
                    skillSlots[j].slotType = SkillSlot.SlotType.Learn;
                    //스킬 슬롯 이미지를 밝게 변경
                    skillSlots[j].unlockImage.SetActive(false);
                    //업그레이드 이미지 비활성화
                    skillSlots[j].upgradeImage.SetActive(false);
                }
            }
        }
    }
}
