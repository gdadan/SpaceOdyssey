using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGameManager : MonoBehaviour
{
    public GameObject QuitPopUp;

    bool isQuitOpen;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        //if (Application.platform == RuntimePlatform.Android)
        {
            if (SceneManager.GetActiveScene().buildIndex != 3)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    QuitPopUpOpen();
                }
            }
        }
    }

    void QuitPopUpOpen()
    {
        isQuitOpen = !isQuitOpen;
        QuitPopUp.SetActive(isQuitOpen);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void QuitPopUpClose()
    {
        SoundManager.instance.PlaySFX(0);

        QuitPopUpOpen();
    }
}
