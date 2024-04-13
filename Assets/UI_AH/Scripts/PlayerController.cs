using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public int controllType; //����� ������ Ÿ��

    public float moveSpeed = 20f; // ������� �̵� �ӵ�

    bool isFar = false; // ��ġ�� ������� �Ÿ� üũ

    Vector3 inputPosition; // ��ġ�� ���� ������
    Vector2 moveDir = Vector2.zero; // ȭ�� ��ġ �� ����� �̵� ����
    Vector2 firstTouchPos; //ó�� ��ġ�� ��

    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //�÷��� üũ
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            PlayerMove();
        }
    }

    void PlayerMove()
    {
        //rigid�� ���ų� ��ġ�� ������ ����
        if (rigid == null || Input.touchCount < 1 || EventSystem.current.IsPointerOverGameObject(0) == true)
            return;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            inputPosition = GetInputPosition(Input.GetTouch(0).position);

            if (controllType == 0)
            {
                if (Vector3.Distance(transform.position, inputPosition) > .4f)
                {
                    isFar = true; //������ ��ġ ������ �Ÿ��� �� ����
                }
            }
            else
            {
                firstTouchPos = GetInputPosition(Input.GetTouch(0).position);
            }
        }

        if (Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            inputPosition = GetInputPosition(Input.GetTouch(0).position);

            if (controllType == 0)
            {
                if (isFar) //�Ÿ��� �� ����
                {
                    moveDir = GetDirection(transform.position, inputPosition);

                    //��ġ�� ����� �Ÿ��� üũ
                    isFar = (Vector3.Distance(transform.position, inputPosition) > .4f);
                }
                else //��ġ�� ����� ����
                {
                    moveDir = Vector2.zero; //���� �ʱ�ȭ
                    transform.position = inputPosition;
                }
            }
        }

        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            inputPosition = GetInputPosition(Input.GetTouch(0).position);

            if (controllType == 0)
            {
                if (isFar) //�Ÿ��� �� ����
                {
                    moveDir = GetDirection(transform.position, inputPosition);

                    //��ġ�� ����� �Ÿ��� üũ
                    isFar = (Vector3.Distance(transform.position, inputPosition) > .4f);
                }
                else //��ġ�� ����� ����
                {
                    moveDir = Vector2.zero; //���� �ʱ�ȭ
                    transform.position = inputPosition;
                }
            }
            else
            {
                Vector2 secondPos = GetInputPosition(Input.GetTouch(0).position);

                Vector2 deltaPos = secondPos - firstTouchPos;

                transform.position += new Vector3(deltaPos.x, deltaPos.y, 0);
                firstTouchPos = secondPos;
            }
        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isFar = false;
            moveDir = Vector2.zero; //���� �ʱ�ȭ
        }

        rigid.velocity = moveDir * moveSpeed * Time.deltaTime; //���⿡ �ӵ��� ���ؼ� rigidbody�� ����
    }

    //��ġ�� ��ũ�� �������� ���� ���������� ����
    public Vector3 GetInputPosition(Vector3 position)
    {
        Vector3 screenPosition = position + (Vector3.back * Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }

    //�� ������ ������ ���� 
    public Vector2 GetDirection(Vector2 from, Vector2 to)
    {
        Vector2 delta = to - from;
        float radian = Mathf.Atan2(delta.y, delta.x);

        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public void ControllTypeChange(int type)
    {
        controllType = type;
    }

    //void PlayerMove()
    //{
    //    if (rigid == null)    //rigid�� ���ų� ��ġ�� ������ ����
    //        return;

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        inputPosition = GetInputPosition(Input.mousePosition);

    //        if (Vector3.Distance(transform.position, inputPosition) > .4f)
    //        {
    //            isFar = true; //������ ��ġ ������ �Ÿ��� �� ����
    //        }

    //        firstTouchPos = GetInputPosition(Input.mousePosition);
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        isFar = false;
    //        moveDir = Vector2.zero; //���� �ʱ�ȭ
    //    }

    //    if (Input.GetMouseButton(0))
    //    {
    //        inputPosition = GetInputPosition(Input.mousePosition);

    //        moveDir = Vector2.zero; //���� �ʱ�ȭ

    //        Vector2 secondPos = GetInputPosition(Input.mousePosition);

    //        Vector2 deltaPos = secondPos - firstTouchPos;

    //        transform.position += new Vector3(deltaPos.x, deltaPos.y, 0);
    //        firstTouchPos = secondPos;
    //    }
    //    rigid.velocity = moveDir * moveSpeed * Time.deltaTime; //���⿡ �ӵ��� ���ؼ� rigid�� ����
    //}
}
