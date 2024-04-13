using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RevivePopUp : MonoBehaviour
{
    DateTime deadTime; //죽었을 때 시간
    DateTime now; //현재 시간

    public GameObject player;
    public GameObject gameOver; //게임 오버 창

    public Image ad_darkImage; //광고버튼 활성화 안됐을 때 이미지
    public Image synergy_darkImage; //시너지 버튼 활성화 안됐을 때 이미지

    public Button adBtn;
    public Button synergyBtn;

    public TextMeshProUGUI timeText; //게임 부활 남은 시간 텍스트
    public TextMeshProUGUI goldText; //플레이어 골드 텍스트

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

    //흘러간 시간 체크
    void TimeCheck()
    {
        TimeSpan timeSpan = now - deadTime;

        timeText.text = (5 - (int)timeSpan.TotalSeconds).ToString();
        if (timeSpan.TotalSeconds >= 5)
        {
            CloseRevivePopUp();
        }
    }
    
    //팝업 Close
    public void CloseRevivePopUp()
    {
        gameObject.SetActive(false);
        gameOver.SetActive(true);
        goldText.text = string.Format("획득한 골드 <sprite=0> {0}", GoldManager.instance.playerGold);
        SoundManager.instance.BGMStop();
        SoundManager.instance.PlaySFX(2);
    }

    //광고 버튼 클릭 시
    public void ADClick()
    {
        RevivePlayer(adBtn, ad_darkImage);
    }

    //시너지 부활 버튼 클릭 시
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
        //플레이어 부활 시 3초간 무적, 부활 버튼 누른 거 비활성화, 팝업 끔
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
