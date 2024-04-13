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
            explainText.text = "���ݷ� ����!";
        }
        if (skillSlot.type == "def")
        {
            explainText.text = "���� ����!";
        }
        if (skillSlot.type == "skill")
        {
            explainText.text = "������ ������ �մϴ�!";
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
            //����� ���� ��ų�� ����
            skillSlot.UnlockedNextSkill();
            //��ų ���� ��� ���·� ����
            skillSlot.slotType = SkillSlot.SlotType.Learn;
            //��ų ���� �̹����� ��� ����
            skillSlot.unlockImage.SetActive(false);
            //���׷��̵� �̹��� ��Ȱ��ȭ
            skillSlot.upgradeImage.SetActive(false);
            //���� ��� �� ����
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
                    //����� ���� ��ų�� ����
                    skillSlots[j].UnlockedNextSkill();
                    //��ų ���� ��� ���·� ����
                    skillSlots[j].slotType = SkillSlot.SlotType.Learn;
                    //��ų ���� �̹����� ��� ����
                    skillSlots[j].unlockImage.SetActive(false);
                    //���׷��̵� �̹��� ��Ȱ��ȭ
                    skillSlots[j].upgradeImage.SetActive(false);
                }
            }
        }
    }
}
