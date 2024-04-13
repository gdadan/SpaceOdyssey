using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MagnetButton : MonoBehaviour
{
    public GameObject darkImage;

    public TextMeshProUGUI countText;

    Button magnetBtn;

    private void Start()
    {
        magnetBtn = GetComponent<Button>();
        magnetBtn.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

        countText.text = DataManager.instance.inGameData.ig_playerItemData[1].itemCount.ToString();

        if (DataManager.instance.inGameData.ig_playerItemData[1].itemCount == 0)
        {
            darkImage.SetActive(true);
            magnetBtn.interactable = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            magnetBtn.onClick.Invoke();
        }
    }

    public void ActiveButton()
    {
        if (DataManager.instance.inGameData.ig_playerItemData[1].itemCount == 0) return;
        else if (DataManager.instance.inGameData.ig_playerItemData[1].itemCount == 1)
        {
            DataManager.instance.inGameData.ig_playerItemData[1].itemCount--;
            darkImage.SetActive(true);
            magnetBtn.interactable = false;
        }
        else
        {
            DataManager.instance.inGameData.ig_playerItemData[1].itemCount--;
        }
        countText.text = DataManager.instance.inGameData.ig_playerItemData[1].itemCount.ToString();

        ItemManager.instance.trans = true;
        StartCoroutine(Tran());
    }

    IEnumerator Tran()
    {
        yield return new WaitForSeconds(StatesManager.instance.Item);
        ItemManager.instance.trans = false;
    }
}