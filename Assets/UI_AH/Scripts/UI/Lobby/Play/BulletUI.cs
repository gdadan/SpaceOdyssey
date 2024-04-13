using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUI : MonoBehaviour
{
    public ObjectPoolUI objectPoolUI;

    [SerializeField] float bulletSpeed; //ÃÑ¾Ë ¼Óµµ
    [SerializeField] int number;

    private void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //BorderBullet¿¡ ´êÀ¸¸é ÃÑ¾Ë ¹ÝÈ¯
        if (collision.gameObject.tag == "BorderBullet")
        {
            objectPoolUI.ReturnObj(number, gameObject);
        }
    }
}
