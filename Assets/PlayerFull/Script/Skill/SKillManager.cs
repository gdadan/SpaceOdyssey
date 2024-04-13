using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SKillManager : MonoBehaviour
{
    public GameObject Skill1;

    public int skillCount;

    float firstAtk;

    public Button skillBtn;

    private void Start()
    {
        skillBtn.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

        firstAtk = StatesManager.instance.skillAtk;

        if (DataManager.instance.playerData.userSkills.Contains(4))
        {
            skillBtn.interactable = true;
            skillCount++;
        }
        if (DataManager.instance.playerData.userSkills.Contains(11))
        {
            skillCount++;
        }
        if (DataManager.instance.playerData.userSkills.Contains(16))
        {
            skillCount++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skillBtn.onClick.Invoke();
        }
    }

    public void SkillStart()
    {
        if (DataManager.instance.playerData.userSkills.Contains(4))
        {
            if (skillCount == 0) return;
            else if (skillCount == 1)
            {
                skillCount--;
                skillBtn.interactable = false;
            }
            else
            {
                skillCount--;
            }
            StartCoroutine(skill1Active());
            SoundManager.instance.PlaySFX(7);
        }
    }

    IEnumerator skill1Active()
    {
        StatesManager.instance.skillAtk = 10;
        Skill1.SetActive(true);

        yield return new WaitForSeconds(1f);

        StatesManager.instance.skillAtk = firstAtk;
        Skill1.SetActive(false);
    }
}
