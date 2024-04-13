using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealingItem : MonoBehaviour
{
    public GameObject darkImage;

    public TextMeshProUGUI countText;

    Button healBtn;

    private void Start()
    {
        healBtn = GetComponent<Button>();
        healBtn.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

        countText.text = DataManager.instance.inGameData.ig_playerItemData[0].itemCount.ToString();

        if (DataManager.instance.inGameData.ig_playerItemData[0].itemCount == 0)
        {
            darkImage.SetActive(true);
            healBtn.interactable = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            healBtn.onClick.Invoke();
        }
    }

    public void Heal()
    {
        if (DataManager.instance.inGameData.ig_playerItemData[0].itemCount == 0) return;
        else if (DataManager.instance.inGameData.ig_playerItemData[0].itemCount == 1)
        {
            DataManager.instance.inGameData.ig_playerItemData[0].itemCount--;
            darkImage.SetActive(true);
            healBtn.interactable = false;
        }
        else
        {
            DataManager.instance.inGameData.ig_playerItemData[0].itemCount--;
        }
        countText.text = DataManager.instance.inGameData.ig_playerItemData[0].itemCount.ToString();

        if (StatesManager.instance.currentHP > (StatesManager.instance.MaxHP - StatesManager.instance.Item * 130))
        {
            StatesManager.instance.currentHP = StatesManager.instance.MaxHP;
        }
        else
        {
            StatesManager.instance.currentHP += StatesManager.instance.Item * 130;
        }
        StatesManager.instance.ChangeHP(0);
    }
}
