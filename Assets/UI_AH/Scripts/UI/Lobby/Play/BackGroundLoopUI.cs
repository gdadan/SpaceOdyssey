using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Ʒ��� ������ �̵��� ����� ���� ������ ���ġ�ϴ� ��ũ��Ʈ
public class BackGroundLoopUI : MonoBehaviour
{
    RectTransform rectTransform; //��� rectTransform
    float height; //����� ���� ����

    void Start()
    {
        Time.timeScale = 1;
        //���� ���� ����
        rectTransform = GetComponent<RectTransform>();
        height = rectTransform.sizeDelta.y;
    }

    void Update()
    {
        // ���� ��ġ�� �������� �Ʒ������� height �̻� �̵������� ��ġ�� ����
        if (rectTransform.anchoredPosition3D.y <= -height)
        {
            RePosition();
        }
    }

    // ��ġ�� �����ϴ� �Լ�
    void RePosition()
    {
        //���� ��ġ���� �������� ���� * 2 ��ŭ �̵�
        Vector2 offset = new Vector2 (0, height * 2f);
        rectTransform.anchoredPosition3D = (Vector2) rectTransform.anchoredPosition3D + offset;
    }
}
