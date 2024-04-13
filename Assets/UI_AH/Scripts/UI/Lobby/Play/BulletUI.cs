using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUI : MonoBehaviour
{
    public ObjectPoolUI objectPoolUI;

    [SerializeField] float bulletSpeed; //�Ѿ� �ӵ�
    [SerializeField] int number;

    private void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //BorderBullet�� ������ �Ѿ� ��ȯ
        if (collision.gameObject.tag == "BorderBullet")
        {
            objectPoolUI.ReturnObj(number, gameObject);
        }
    }
}
