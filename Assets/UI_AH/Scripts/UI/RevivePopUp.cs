using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RevivePopUp : MonoBehaviour
{
    DateTime deadTime; //�׾��� �� �ð�
    DateTime now; //���� �ð�

    public GameObject player;
    public GameObject gameOver; //���� ���� â

    public Image ad_darkImage; //�����ư Ȱ��ȭ �ȵ��� �� �̹���
    public Image synergy_darkImage; //�ó��� ��ư Ȱ��ȭ �ȵ��� �� �̹���

    public Button adBtn;
    public Button synergyBtn;

    public TextMeshProUGUI timeText; //���� ��Ȱ ���� �ð� �ؽ�Ʈ
    public TextMeshProUGUI goldText; //�÷��̾� ��� �ؽ�Ʈ

    private void Update()
    {
        now = DateTime.Now;

        TimeCheck();
    }

    private void OnEnable()
    {
        deadTime = DateTime.Now;

        if (StatesManager.instance.Revive == 1)
        {
            synergyBtn.interactable = true;
            synergy_darkImage.gameObject.SetActive(false);
        }
    }

    //�귯�� �ð� üũ
    void TimeCheck()
    {
        TimeSpan timeSpan = now - deadTime;

        timeText.text = (5 - (int)timeSpan.TotalSeconds).ToString();
        if (timeSpan.TotalSeconds >= 5)
        {
            CloseRevivePopUp();
        }
    }
    
    //�˾� Close
    public void CloseRevivePopUp()
    {
        gameObject.SetActive(false);
        gameOver.SetActive(true);
        goldText.text = string.Format("ȹ���� ��� <sprite=0> {0}", GoldManager.instance.playerGold);
        SoundManager.instance.BGMStop();
        SoundManager.instance.PlaySFX(2);
    }

    //���� ��ư Ŭ�� ��
    public void ADClick()
    {
        RevivePlayer(adBtn, ad_darkImage);
    }

    //�ó��� ��Ȱ ��ư Ŭ�� ��
    public void SynergyClick()
    {
        if (StatesManager.instance.Revive == 1)
        {
            StatesManager.instance.Revive--;
            RevivePlayer(synergyBtn, synergy_darkImage);
        }
    }

    void RevivePlayer(Button clickedBtn, Image clickedImage)
    {
        //�÷��̾� ��Ȱ �� 3�ʰ� ����, ��Ȱ ��ư ���� �� ��Ȱ��ȭ, �˾� ��
        player.tag = "Invincibility";
        SoundManager.instance.PlaySFX(0);
        StatesManager.instance.currentHP = StatesManager.instance.MaxHP;
        clickedBtn.interactable = false;
        clickedImage.gameObject.SetActive(true);
        gameObject.SetActive(false);
        Time.timeScale = 1;
        Invoke("ChangeTag", 3f);
    }


    void ChangeTag()
    {
        player.tag = "Player";
    }
}
