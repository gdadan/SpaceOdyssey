using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollingBackGroudUI : MonoBehaviour
{
    public float speed; //��� �������� �ӵ�
   
    void Update()
    {
        //ȭ�鿡 �������� �����̰� �ϱ�
        if (SceneManager.GetActiveScene().buildIndex == 0 || NestedScrollManager.instance.targetIndex == 2)
        {
            //���� ������Ʈ�� �Ʒ������� ���� �ӵ� �����̵�
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }
}
