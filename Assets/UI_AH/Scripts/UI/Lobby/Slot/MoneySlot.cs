using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MoneySlot : MonoBehaviour
{
    public string moneyType; //��ȭ ����
    public int moneyCount; //��ȭ ��
    public int moneyPrice; //��ȭ ����

    public Image moneyImage; //��ȭ �̹���

    Button moneyBtn;

    public TextMeshProUGUI moneyCountText;
    public TextMeshProUGUI moneyPriceText;

    void Start()
    {
        //������ ��ȭ�� ������ ���� �ؽ�Ʈ �ֱ�
        moneyCountText.text = moneyCount.ToString();
        moneyPriceText.text = moneyPrice.ToString();

        moneyBtn = GetComponent<Button>();
        moneyBtn.onClick.AddListener(() => OnClickMoneyBtn());
    }

    //��ȭ ���� �˾�â Ȱ��ȭ
    public void OnClickMoneyBtn()
    {
        SoundManager.instance.PlaySFX(0);

        ShopManager.instance.moneyPopUp.SetActive(true);
        ShopManager.instance.moneySlot = this;
        ShopManager.instance.MoneyPopUpUI();
    }
}
