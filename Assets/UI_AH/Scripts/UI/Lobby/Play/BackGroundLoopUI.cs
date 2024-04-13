using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아래쪽 끝으로 이동한 배경을 위쪽 끝으로 재배치하는 스크립트
public class BackGroundLoopUI : MonoBehaviour
{
    RectTransform rectTransform; //배경 rectTransform
    float height; //배경의 세로 길이

    void Start()
    {
        Time.timeScale = 1;
        //세로 길이 측정
        rectTransform = GetComponent<RectTransform>();
        height = rectTransform.sizeDelta.y;
    }

    void Update()
    {
        // 현재 위치가 원점에서 아래쪽으로 height 이상 이동했을때 위치를 리셋
        if (rectTransform.anchoredPosition3D.y <= -height)
        {
            RePosition();
        }
    }

    // 위치를 리셋하는 함수
    void RePosition()
    {
        //현재 위치에서 위쪽으로 세로 * 2 만큼 이동
        Vector2 offset = new Vector2 (0, height * 2f);
        rectTransform.anchoredPosition3D = (Vector2) rectTransform.anchoredPosition3D + offset;
    }
}
