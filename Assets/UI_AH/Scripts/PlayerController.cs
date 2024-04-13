using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public int controllType; //비행기 움직임 타입

    public float moveSpeed = 20f; // 비행기의 이동 속도

    bool isFar = false; // 터치와 비행기의 거리 체크

    Vector3 inputPosition; // 터치의 월드 포지션
    Vector2 moveDir = Vector2.zero; // 화면 터치 시 비행기 이동 방향
    Vector2 firstTouchPos; //처음 터치한 곳

    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //플랫폼 체크
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            PlayerMove();
        }
    }

    void PlayerMove()
    {
        //rigid가 없거나 터치가 없으면 리턴
        if (rigid == null || Input.touchCount < 1 || EventSystem.current.IsPointerOverGameObject(0) == true)
            return;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            inputPosition = GetInputPosition(Input.GetTouch(0).position);

            if (controllType == 0)
            {
                if (Vector3.Distance(transform.position, inputPosition) > .4f)
                {
                    isFar = true; //비행기와 터치 사이의 거리가 먼 상태
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
                if (isFar) //거리가 먼 상태
                {
                    moveDir = GetDirection(transform.position, inputPosition);

                    //터치와 비행기 거리를 체크
                    isFar = (Vector3.Distance(transform.position, inputPosition) > .4f);
                }
                else //터치에 가까운 상태
                {
                    moveDir = Vector2.zero; //방향 초기화
                    transform.position = inputPosition;
                }
            }
        }

        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            inputPosition = GetInputPosition(Input.GetTouch(0).position);

            if (controllType == 0)
            {
                if (isFar) //거리가 먼 상태
                {
                    moveDir = GetDirection(transform.position, inputPosition);

                    //터치와 비행기 거리를 체크
                    isFar = (Vector3.Distance(transform.position, inputPosition) > .4f);
                }
                else //터치에 가까운 상태
                {
                    moveDir = Vector2.zero; //방향 초기화
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
            moveDir = Vector2.zero; //방향 초기화
        }

        rigid.velocity = moveDir * moveSpeed * Time.deltaTime; //방향에 속도를 곱해서 rigidbody에 적용
    }

    //터치의 스크린 포지션을 월드 포지션으로 변경
    public Vector3 GetInputPosition(Vector3 position)
    {
        Vector3 screenPosition = position + (Vector3.back * Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }

    //두 포지션 사이의 방향 
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
    //    if (rigid == null)    //rigid가 없거나 터치가 없으면 리턴
    //        return;

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        inputPosition = GetInputPosition(Input.mousePosition);

    //        if (Vector3.Distance(transform.position, inputPosition) > .4f)
    //        {
    //            isFar = true; //비행기와 터치 사이의 거리가 먼 상태
    //        }

    //        firstTouchPos = GetInputPosition(Input.mousePosition);
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        isFar = false;
    //        moveDir = Vector2.zero; //방향 초기화
    //    }

    //    if (Input.GetMouseButton(0))
    //    {
    //        inputPosition = GetInputPosition(Input.mousePosition);

    //        moveDir = Vector2.zero; //방향 초기화

    //        Vector2 secondPos = GetInputPosition(Input.mousePosition);

    //        Vector2 deltaPos = secondPos - firstTouchPos;

    //        transform.position += new Vector3(deltaPos.x, deltaPos.y, 0);
    //        firstTouchPos = secondPos;
    //    }
    //    rigid.velocity = moveDir * moveSpeed * Time.deltaTime; //방향에 속도를 곱해서 rigid에 적용
    //}
}
