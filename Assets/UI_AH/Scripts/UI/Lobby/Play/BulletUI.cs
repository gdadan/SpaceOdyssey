using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUI : MonoBehaviour
{
    public ObjectPoolUI objectPoolUI;

    [SerializeField] float bulletSpeed; //총알 속도
    [SerializeField] int number;

    private void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //BorderBullet에 닿으면 총알 반환
        if (collision.gameObject.tag == "BorderBullet")
        {
            objectPoolUI.ReturnObj(number, gameObject);
        }
    }
}
