using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NestedScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Scrollbar scrollbar; //횡스크롤바
    public Slider tabSlider; //하단 탭 슬라이더
    public Transform contentTr;
    public RectTransform[] btnRect;
    public RectTransform[] btnImageRect;

    const int size = 4; //화면 개수

    float[] pos = new float[size];
    float distance; //pos사이의 거리
    float targetPos;
    float curPos;
    bool isDragging;
    
    public int targetIndex;

    public static NestedScrollManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //거리에 따라 0~1인 pos대입 (4개면 0, 0.33, 0.66, 1)
        distance = 1f / (size - 1);
        for (int i = 0; i < size; i++)
        {
            pos[i] = distance * i;
        }

        //처음 시작 시 플레이화면을 띄움
        GetComponent<ScrollRect>().horizontalScrollbar.value = 0.6666f;
        scrollbar.value = 0.6666f;
        tabSlider.value = 0.6666f;
        TabClick(2);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        curPos = SetPos();
        //print(curPos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging=false;

        targetPos = SetPos();
        //print(targetPos);

        //절반 거리를 넘지 않아도 화면을 빠르게 이동하면
        if (curPos == targetPos)
        {
            //print(eventData.delta.x);

            //스크롤을 왼쪽으로 빠르게 이동하면 targetIndex가 하나 감소
            if (eventData.delta.x > 18 && curPos - distance >= 0)
            {
                --targetIndex;
                targetPos = curPos - distance;
            }
            //스크롤을 오른쪽으로 빠르게 이동하면 targetIndex가 하나 증가
            if (eventData.delta.x < -18 && curPos + distance <= 1.01f)
            {
                ++targetIndex;
                targetPos = curPos + distance;
            }
        }
        VerticalScrollUp();
    }

    void VerticalScrollUp()
    {
        //목표가 수직 스크롤이고, 옆에서 옮겨왔다면 수직 스크롤을 맨 위로 올림
        for (int i = 0; i < size; i++)
        {
            if (contentTr.GetChild(i).GetComponent<ScrollScript>() && curPos != pos[i] && targetPos == pos[i])
            {
                contentTr.GetChild(i).GetChild(1).GetComponent<Scrollbar>().value = 1;
            }
        }
    }

    void Update()
    {
        tabSlider.value = scrollbar.value;

        if(!isDragging)
        {
            //화면에서 손을 떼면 부드럽게 이동
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);

            //선택한 버튼은 크기가 가로로 커짐
            for (int i = 0; i < size; i++)
            {
                btnRect[i].sizeDelta = new Vector2(i == targetIndex ? 360 : 180, btnRect[i].sizeDelta.y);
            }
        }
        //처음에 이미지 모이는 현상 제거
        if (Time.time < 0.1f)
            return;

        for (int i = 0; i < size; i++)
        {
            //버튼 아이콘이 부드럽게 버튼의 중앙으로 이동, 크기는 0.8, 텍스트 비활성화
            Vector3 pos = new Vector3(20,0,0); //이미지 위치 조정을 위한 pos
            Vector3 btnTargetPos = btnRect[i].anchoredPosition3D + pos;
            Vector3 btnTargetScale = new Vector3(0.8f, 0.8f, 1);
            bool textActive = false;

            if (i == targetIndex)
            {
                //선택한 버튼 아이콘은 약간 위로 올리고, 크기도 키우고, 텍스트도 활성화
                btnTargetPos.x = btnTargetPos.x + 70f;
                btnTargetPos.y = 40f;
                btnTargetScale = Vector3.one;
                textActive = true;
            }
            btnImageRect[i].anchoredPosition3D = Vector3.Lerp(btnImageRect[i].anchoredPosition3D, btnTargetPos, 0.25f);
            btnImageRect[i].localScale = Vector3.Lerp(btnImageRect[i].localScale, btnTargetScale, 0.25f);
            btnImageRect[i].transform.GetChild(0).gameObject.SetActive(textActive);
        }
    }

    float SetPos()
    {
        //절반 거리를 기준으로 가까운 위치로 반환
        for (int i = 0; i < size; i++)
        {
            if (scrollbar.value < pos[i] + distance * 0.5f && scrollbar.value > pos[i] - distance * 0.5f)
            {
                targetIndex = i;
                return pos[i];
            }
        }
        return 0;
    }

    public void TabClick(int n)
    {
        SoundManager.instance.PlaySFX(0);
        targetIndex = n;
        targetPos = pos[n];
        if (contentTr.GetChild(n).GetComponent<ScrollScript>() && targetPos == pos[n])
        {
            contentTr.GetChild(n).GetChild(1).GetComponent<Scrollbar>().value = 1;
        }
    }
   
    public void GoldClick()
    {
        GetComponent<ScrollRect>().horizontalScrollbar.value = 0f;
        scrollbar.value = 0f;
        tabSlider.value = 0f;
        TabClick(0);
    }
}
