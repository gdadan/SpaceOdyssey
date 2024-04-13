using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolUI : MonoBehaviour
{
    public List<GameObject> bulletsPrefab; //�Ѿ� �����յ�
    public List<Queue<GameObject>> bullets; //�Ѿ˵�

    private void Awake()
    {
        bullets = new List<Queue<GameObject>>();

        //�Ѿ� �����ռ���ŭ ��� �ִ� queue ����
        for (int i = 0; i < bulletsPrefab.Count; i++)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            bullets.Add(queue);
        }
    }

    //�Ѿ� ���� �Լ�
    GameObject Create(int num)
    {
        GameObject bullet = Instantiate(bulletsPrefab[num]);
        bullet.GetComponent<BulletUI>().objectPoolUI = this;
        bullet.SetActive(false);
        return bullet;
    }

    //�Ѿ� �������� �Լ�
    public GameObject GetObj(int num)
    {
        if (bullets[num].Count > 0)
        {
            var obj = bullets[num].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            var newobj = Create(num);
            newobj.SetActive(true);
            return newobj;
        }
    }

    //�Ѿ� ��ȯ �Լ�
    public void ReturnObj(int num, GameObject gameObject)
    {
        gameObject.SetActive(false);
        bullets[num].Enqueue(gameObject);
    }
}
