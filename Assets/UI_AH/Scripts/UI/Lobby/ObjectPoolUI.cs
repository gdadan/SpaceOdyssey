using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolUI : MonoBehaviour
{
    public List<GameObject> bulletsPrefab; //총알 프리팹들
    public List<Queue<GameObject>> bullets; //총알들

    private void Awake()
    {
        bullets = new List<Queue<GameObject>>();

        //총알 프리팹수만큼 비어 있는 queue 생성
        for (int i = 0; i < bulletsPrefab.Count; i++)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            bullets.Add(queue);
        }
    }

    //총알 생성 함수
    GameObject Create(int num)
    {
        GameObject bullet = Instantiate(bulletsPrefab[num]);
        bullet.GetComponent<BulletUI>().objectPoolUI = this;
        bullet.SetActive(false);
        return bullet;
    }

    //총알 가져오는 함수
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

    //총알 반환 함수
    public void ReturnObj(int num, GameObject gameObject)
    {
        gameObject.SetActive(false);
        bullets[num].Enqueue(gameObject);
    }
}
