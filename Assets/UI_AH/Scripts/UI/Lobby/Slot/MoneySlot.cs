using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MoneySlot : MonoBehaviour
{
    public string moneyType; //재화 종류
    public int moneyCount; //재화 수
    public int moneyPrice; //재화 가격

    public Image moneyImage; //재화 이미지

    Button moneyBtn;

    public TextMeshProUGUI moneyCountText;
    public TextMeshProUGUI moneyPriceText;

    void Start()
    {
        //각각의 재화에 수량과 가격 텍스트 넣기
        moneyCountText.text = moneyCount.ToString();
        moneyPriceText.text = moneyPrice.ToString();

        moneyBtn = GetComponent<Button>();
        moneyBtn.onClick.AddListener(() => OnClickMoneyBtn());
    }

    //재화 구매 팝업창 활성화
    public void OnClickMoneyBtn()
    {
        SoundManager.instance.PlaySFX(0);

        ShopManager.instance.moneyPopUp.SetActive(true);
        ShopManager.instance.moneySlot = this;
        ShopManager.instance.MoneyPopUpUI();
    }
}
