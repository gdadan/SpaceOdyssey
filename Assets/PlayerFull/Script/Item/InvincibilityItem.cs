using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvincibilityItem : MonoBehaviour
{
    public GameObject player;
    public GameObject invincibility; //���� ȿ��
    public GameObject darkImage; //��ư ��ο� �̹���

    public TextMeshProUGUI countText; //������ ���� ��

    Button invincibilityBtn; //���� ��ư

    private void Start()
    {
        invincibilityBtn = GetComponent<Button>();
        invincibilityBtn.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

        countText.text = DataManager.instance.inGameData.ig_playerItemData[2].itemCount.ToString();

        //������ ���� ���� 0�̸� ��ư��Ӱ�, ��Ȱ��ȭ
        if (DataManager.instance.inGameData.ig_playerItemData[2].itemCount == 0)
        {
            darkImage.SetActive(true);
            invincibilityBtn.interactable = false;
        }
    }

    private void Update()
    {
        //c ��ư�� ������ ���� ������ ���
        if (Input.GetKeyDown(KeyCode.C))
        {
            invincibilityBtn.onClick.Invoke();
        }
    }

    public void Change()
    {
        if (DataManager.instance.inGameData.ig_playerItemData[2].itemCount == 0) return;
        else if (DataManager.instance.inGameData.ig_playerItemData[2].itemCount == 1)
        {
            DataManager.instance.inGameData.ig_playerItemData[2].itemCount--;
            darkImage.SetActive(true);
            invincibilityBtn.interactable = false;
        }
        else
        {
            DataManager.instance.inGameData.ig_playerItemData[2].itemCount--;
        }
        countText.text = DataManager.instance.inGameData.ig_playerItemData[2].itemCount.ToString();

        player.tag = "Invincibility";
        invincibility.SetActive(true);

        StartCoroutine(ChangeTag());
    }

    IEnumerator ChangeTag()
    {
        yield return new WaitForSeconds(StatesManager.instance.Item);

        player.tag = "Player";
        invincibility.SetActive(false);
    }
}
