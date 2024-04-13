using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NestedScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Scrollbar scrollbar; //Ⱦ��ũ�ѹ�
    public Slider tabSlider; //�ϴ� �� �����̴�
    public Transform contentTr;
    public RectTransform[] btnRect;
    public RectTransform[] btnImageRect;

    const int size = 4; //ȭ�� ����

    float[] pos = new float[size];
    float distance; //pos������ �Ÿ�
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
        //�Ÿ��� ���� 0~1�� pos���� (4���� 0, 0.33, 0.66, 1)
        distance = 1f / (size - 1);
        for (int i = 0; i < size; i++)
        {
            pos[i] = distance * i;
        }

        //ó�� ���� �� �÷���ȭ���� ���
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

        //���� �Ÿ��� ���� �ʾƵ� ȭ���� ������ �̵��ϸ�
        if (curPos == targetPos)
        {
            //print(eventData.delta.x);

            //��ũ���� �������� ������ �̵��ϸ� targetIndex�� �ϳ� ����
            if (eventData.delta.x > 18 && curPos - distance >= 0)
            {
                --targetIndex;
                targetPos = curPos - distance;
            }
            //��ũ���� ���������� ������ �̵��ϸ� targetIndex�� �ϳ� ����
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
        //��ǥ�� ���� ��ũ���̰�, ������ �ŰܿԴٸ� ���� ��ũ���� �� ���� �ø�
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
            //ȭ�鿡�� ���� ���� �ε巴�� �̵�
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);

            //������ ��ư�� ũ�Ⱑ ���η� Ŀ��
            for (int i = 0; i < size; i++)
            {
                btnRect[i].sizeDelta = new Vector2(i == targetIndex ? 360 : 180, btnRect[i].sizeDelta.y);
            }
        }
        //ó���� �̹��� ���̴� ���� ����
        if (Time.time < 0.1f)
            return;

        for (int i = 0; i < size; i++)
        {
            //��ư �������� �ε巴�� ��ư�� �߾����� �̵�, ũ��� 0.8, �ؽ�Ʈ ��Ȱ��ȭ
            Vector3 pos = new Vector3(20,0,0); //�̹��� ��ġ ������ ���� pos
            Vector3 btnTargetPos = btnRect[i].anchoredPosition3D + pos;
            Vector3 btnTargetScale = new Vector3(0.8f, 0.8f, 1);
            bool textActive = false;

            if (i == targetIndex)
            {
                //������ ��ư �������� �ణ ���� �ø���, ũ�⵵ Ű���, �ؽ�Ʈ�� Ȱ��ȭ
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
        //���� �Ÿ��� �������� ����� ��ġ�� ��ȯ
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
