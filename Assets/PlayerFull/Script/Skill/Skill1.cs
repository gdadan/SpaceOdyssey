using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {               
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().health -= Bullet.instance.finalAtk;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().health -= Bullet.instance.finalAtk;
        }
    }
}
