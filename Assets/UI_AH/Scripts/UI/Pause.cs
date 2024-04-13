using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pausePopUp; //일시정지 팝업

    public Button pauseBtn;

    private void Update()
    {
        //게임씬에서 esc 버튼을 누르면 일시정지 팝업뜸
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
