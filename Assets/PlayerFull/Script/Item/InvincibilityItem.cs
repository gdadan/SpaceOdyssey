using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvincibilityItem : MonoBehaviour
{
    public GameObject player;
    public GameObject invincibility; //무적 효과
    public GameObject darkImage; //버튼 어두운 이미지

    public TextMeshProUGUI countText; //아이템 남은 수

    Button invincibilityBtn; //무적 버튼

    private void Start()
    {
        invincibilityBtn = GetComponent<Button>();
        invincibilityBtn.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

        countText.text = DataManager.instance.inGameData.ig_playerItemData[2].itemCount.ToString();

        //아이템 남은 수가 0이면 버튼어둡게, 비활성화
        if (DataManager.instance.inGameData.ig_playerItemData[2].itemCount == 0)
        {
            darkImage.SetActive(true);
            invincibilityBtn.interactable = false;
        }
    }

    private void Update()
    {
        //c 버튼을 누르면 무적 아이템 사용
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
