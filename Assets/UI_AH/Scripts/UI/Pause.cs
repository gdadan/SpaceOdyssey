using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pausePopUp; //�Ͻ����� �˾�

    public Button pauseBtn;

    private void Update()
    {
        //���Ӿ����� esc ��ư�� ������ �Ͻ����� �˾���
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseBtn.onClick.Invoke();
            }
        }
    }
    public void OnClickPauseBtn()
    {
        Time.timeScale = 0;
        pausePopUp.SetActive(true);
    }

    public void OnClickResumeBtn()
    {
        pausePopUp.SetActive(false);
        Time.timeScale = 1;
    }
}
