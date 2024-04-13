using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollingBackGroudUI : MonoBehaviour
{
    public float speed; //배경 내려가는 속도
   
    void Update()
    {
        //화면에 있을때만 움직이게 하기
        if (SceneManager.GetActiveScene().buildIndex == 0 || NestedScrollManager.instance.targetIndex == 2)
        {
            //게임 오브젝트가 아래쪽으로 일정 속도 평행이동
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }
}
