using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyManager : MonoBehaviour
{
    public static SynergyManager instance;

    public GameObject shield;
    int shieldIndex;

    private void Awake()
    {
        instance = this; 
    }
    public void Synergy()
    {
        foreach(string s in BulletsManager.instance.synergys.Keys)
        {
            switch (s)
            {
                case "공격력":
                    AttackPower();
                    break;

                case "최대체력":
                    MaxHealth();
                    break;

                case "발사속도":
                    Speed();
                    break;

                case "회복":
                    Healing();
                    break;

                case "아이템효과증가":
                    Item();
                    break;

                case "부활":
                    Revive();
                    break;

                case "방어막":
                    Defence();
                    break;
            }
        }
    }

    void AttackPower()
    {

        if (BulletsManager.instance.synergys["공격력"] == 1)
        {
            StatesManager.instance.synergyAtk = 1.5f;
        }
        else if (BulletsManager.instance.synergys["공격력"] == 2)
        {
            StatesManager.instance.synergyAtk = 2f;
        }
        else if(BulletsManager.instance.synergys["공격력"] == 3)
        {
            StatesManager.instance.synergyAtk = 3f;
        }
        else
        {
            StatesManager.instance.synergyAtk = 1f;
        }
    }

    void MaxHealth()
    {
        if (BulletsManager.instance.synergys["최대체력"] == 2)
        {
            StatesManager.instance.MaxHP = 1500;
        }
        else if (BulletsManager.instance.synergys["최대체력"] == 3)
        {
            StatesManager.instance.MaxHP = 2000;
        }
        else if (BulletsManager.instance.synergys["최대체력"] == 4)
        {
            StatesManager.instance.MaxHP = 3000;
        }
        else
        {
            StatesManager.instance.MaxHP = 1000;
        }

    }

    void Speed()
    {
        if (BulletsManager.instance.synergys["발사속도"] == 2)
        {
            StatesManager.instance.Speed = 0.55f;
        }
        else if (BulletsManager.instance.synergys["발사속도"] == 3)
        {
            StatesManager.instance.Speed = 0.45f;
        }
        else if (BulletsManager.instance.synergys["발사속도"] == 4)
        {
            StatesManager.instance.Speed = 0.3f;
        }
        else
        {
            StatesManager.instance.Speed = 0.7f;
        }
    }

    void Healing()
    {
        if (BulletsManager.instance.synergys["회복"] == 2)
        {
            StatesManager.instance.Healing = 3f;
        }
        else if (BulletsManager.instance.synergys["회복"] == 3)
        {
            StatesManager.instance.Healing = 5f;
        }
        else if (BulletsManager.instance.synergys["회복"] == 4)
        {
            StatesManager.instance.Healing = 7f;
        }
        else
        {
            StatesManager.instance.Healing = 0f;
        }
    }

    void Item()
    {
        if (BulletsManager.instance.synergys["아이템효과증가"] == 1)
        {
            StatesManager.instance.Item = 4f;
        }
        else if (BulletsManager.instance.synergys["아이템효과증가"] == 2)
        {
            StatesManager.instance.Item = 6f;
        }
        else if (BulletsManager.instance.synergys["아이템효과증가"] == 3)
        {
            StatesManager.instance.Item = 9f;
        }
        else
        {
            StatesManager.instance.Item = 3f;
        }
    }

    void Revive()
    {
        if (BulletsManager.instance.synergys["부활"] == 3)
        {
            StatesManager.instance.Revive = 1;
        }
    }

    void Defence()
    {
        if (BulletsManager.instance.synergys["방어막"] == 1)
        {
            StatesManager.instance.MaxDefence = 30;
            shieldIndex = 0;
            OnDefence(shieldIndex);
        }
        else if (BulletsManager.instance.synergys["방어막"] == 2)
        {
            StatesManager.instance.MaxDefence = 50;
            shieldIndex = 1;
            OnDefence(shieldIndex);

        }
        else if (BulletsManager.instance.synergys["방어막"] == 3)
        {
            StatesManager.instance.MaxDefence = 100;
            shieldIndex = 2;
            OnDefence(shieldIndex);
        }
        else
        {
            StatesManager.instance.MaxDefence = 0;
            shield.SetActive(false);
        }
    }

    void OnDefence(int activeIndex)
    {
        shield.SetActive(true);
        for (int i = 0; i < shield.transform.childCount; i++)
        {
            shield.transform.GetChild(i).gameObject.SetActive(false);
        }
        shield.transform.GetChild(activeIndex).gameObject.SetActive(true);
    }
}
