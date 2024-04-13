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
                case "���ݷ�":
                    AttackPower();
                    break;

                case "�ִ�ü��":
                    MaxHealth();
                    break;

                case "�߻�ӵ�":
                    Speed();
                    break;

                case "ȸ��":
                    Healing();
                    break;

                case "������ȿ������":
                    Item();
                    break;

                case "��Ȱ":
                    Revive();
                    break;

                case "��":
                    Defence();
                    break;
            }
        }
    }

    void AttackPower()
    {

        if (BulletsManager.instance.synergys["���ݷ�"] == 1)
        {
            StatesManager.instance.synergyAtk = 1.5f;
        }
        else if (BulletsManager.instance.synergys["���ݷ�"] == 2)
        {
            StatesManager.instance.synergyAtk = 2f;
        }
        else if(BulletsManager.instance.synergys["���ݷ�"] == 3)
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
        if (BulletsManager.instance.synergys["�ִ�ü��"] == 2)
        {
            StatesManager.instance.MaxHP = 1500;
        }
        else if (BulletsManager.instance.synergys["�ִ�ü��"] == 3)
        {
            StatesManager.instance.MaxHP = 2000;
        }
        else if (BulletsManager.instance.synergys["�ִ�ü��"] == 4)
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
        if (BulletsManager.instance.synergys["�߻�ӵ�"] == 2)
        {
            StatesManager.instance.Speed = 0.55f;
        }
        else if (BulletsManager.instance.synergys["�߻�ӵ�"] == 3)
        {
            StatesManager.instance.Speed = 0.45f;
        }
        else if (BulletsManager.instance.synergys["�߻�ӵ�"] == 4)
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
        if (BulletsManager.instance.synergys["ȸ��"] == 2)
        {
            StatesManager.instance.Healing = 3f;
        }
        else if (BulletsManager.instance.synergys["ȸ��"] == 3)
        {
            StatesManager.instance.Healing = 5f;
        }
        else if (BulletsManager.instance.synergys["ȸ��"] == 4)
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
        if (BulletsManager.instance.synergys["������ȿ������"] == 1)
        {
            StatesManager.instance.Item = 4f;
        }
        else if (BulletsManager.instance.synergys["������ȿ������"] == 2)
        {
            StatesManager.instance.Item = 6f;
        }
        else if (BulletsManager.instance.synergys["������ȿ������"] == 3)
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
        if (BulletsManager.instance.synergys["��Ȱ"] == 3)
        {
            StatesManager.instance.Revive = 1;
        }
    }

    void Defence()
    {
        if (BulletsManager.instance.synergys["��"] == 1)
        {
            StatesManager.instance.MaxDefence = 30;
            shieldIndex = 0;
            OnDefence(shieldIndex);
        }
        else if (BulletsManager.instance.synergys["��"] == 2)
        {
            StatesManager.instance.MaxDefence = 50;
            shieldIndex = 1;
            OnDefence(shieldIndex);

        }
        else if (BulletsManager.instance.synergys["��"] == 3)
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
