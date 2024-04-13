using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public enum SlotType
    {
        Lock,
        Unlock,
        Learn
    }

    public SlotType slotType = SlotType.Lock;
   
    public List<SkillSlot> nextSkillSlots;

    public GameObject unlockImage;
    public GameObject upgradeImage;

    public int key; //아이템 key값
    public int price;
    public string type;

    public Sprite skillSprite;
    public Image image;

    public Color color;

    Button skillBtn;

    void Start()
    {
        skillBtn = GetComponent<Button>();
        skillBtn.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        skillBtn.onClick.AddListener(() => OnClickSkillBtn());
    }

    void OnClickSkillBtn()
    {
        SoundManager.instance.PlaySFX(0);

        switch (slotType)
        {
            case SkillSlot.SlotType.Lock:
                break;
            case SkillSlot.SlotType.Unlock:
                SkilltreeManager.instance.OpenItemPopUp(this, true);
                break;
            case SkillSlot.SlotType.Learn:
                SkilltreeManager.instance.OpenItemPopUp(this, false);
                break;
        }
    }

    public void UnlockedNextSkill()
    {
        for (int i = 0; i < nextSkillSlots.Count; i++)
        {
            if (nextSkillSlots[i].slotType == SlotType.Lock)
            {
                nextSkillSlots[i].image.sprite = nextSkillSlots[i].skillSprite;
                nextSkillSlots[i].image.color = nextSkillSlots[i].color;
                nextSkillSlots[i].slotType = SkillSlot.SlotType.Unlock;
                nextSkillSlots[i].upgradeImage.SetActive(true);
                nextSkillSlots[i].GetComponent<Button>().interactable = true;
            }
        }
    }
}
