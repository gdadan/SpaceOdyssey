using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class StatesManager : MonoBehaviour
{
    public static StatesManager instance;

    public GameObject Relive;
    public GameObject hitPlayer;

    public Image progressBarHP;
    public Image progressBarDefence;

    public TextMeshProUGUI HPText;
    public TextMeshProUGUI defenceText;

    public float Def; //방어력

    public float skillAtk; //스킬 공격력
    public float skillDef; //스킬 방어력

    public float currentHP; //현재 체력
    public float MaxHP; //최대 체력

    public float Speed; //발사속도
    public float Healing; //회복
    public float Item; //아이템 지속시간
    public int Revive; //부활
    public float Defence; //방어막
    public float MaxDefence;

    public float synergyAtk; //시너지 공격력

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentHP = MaxHP;
        Defence = MaxDefence;
        skillAtk += DataManager.instance.playerData.skillAtk;
        ChangeHP(0);
        SumDef();
        StartCoroutine(OnHealing());
    }

    public void SumDef()
    {
        Def = 0;

        for (int i = 0; i < BulletsManager.instance.bulletIndexs.Length; i++)
        {
            if (BulletsManager.instance.bulletIndexs[i].HasValue)
            {
                switch (i)
                {
                    case 0:
                        Def += BulletsManager.instance.bullet1[(int)BulletsManager.instance.bulletIndexs[i]].def;
                        break;
                    case 1:
                        Def += BulletsManager.instance.bullet2[(int)BulletsManager.instance.bulletIndexs[i]].def;
                        break;
                    case 2:
                        Def += BulletsManager.instance.bullet3[(int)BulletsManager.instance.bulletIndexs[i]].def;
                        break;
                    case 3:
                        Def += BulletsManager.instance.engine1[(int)BulletsManager.instance.bulletIndexs[i]].def;
                        break;
                    case 4:
                        Def += BulletsManager.instance.engine2[(int)BulletsManager.instance.bulletIndexs[i]].def;
                        break;
                }
            }
        }
    }

    IEnumerator OnHealing()
    {
        while (true)
        {
            if (currentHP < MaxHP)
            {
                currentHP += Healing;
            }
            ChangeHP(0);
            yield return new WaitForSeconds(1f);
        }
    }

    public void ChangeHP(float damage)
    {
        if (Defence > 0)
        {
            Defence -= damage;
            SynergyManager.instance.shield.SetActive(true);
        }
        else if (Defence <= 0)
        {
            Defence = 0;

            SynergyManager.instance.shield.SetActive(false);

            currentHP -= damage;
        }

        if (currentHP < 0)
        {
            currentHP = 0;
        }
        if (currentHP > MaxHP)
        {
            currentHP = MaxHP;
        }
        progressBarHP.fillAmount = currentHP / MaxHP;

        if (MaxDefence == 0) progressBarDefence.fillAmount = 0;
        else progressBarDefence.fillAmount = Defence / MaxDefence;

        HPText.text = (currentHP + " / " + MaxHP).ToString();
        defenceText.text = Defence.ToString();
    }

    public void Death()
    {
        if (currentHP <= 0)
        {
            Relive.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ChangeMaterial()
    {
        StartCoroutine(Hit());
    }

    IEnumerator Hit()
    {
        hitPlayer.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitPlayer.SetActive(false);
    }
}
