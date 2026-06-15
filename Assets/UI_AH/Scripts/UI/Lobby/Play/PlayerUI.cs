using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] float bullet1CoolTime; //총알C 쿨타임
    [SerializeField] float bullet2CoolTime; //총알B 쿨타임
    [SerializeField] float bullet3CoolTime; //총알A 쿨타임
    
    float bullet1Timer;
    float bullet2Timer;
    float bullet3Timer;

    public Transform bullet1Tr;
    public Transform bulletR2Tr;
    public Transform bulletR3Tr;
    public Transform bulletL2Tr;
    public Transform bulletL3Tr;

    public ObjectPoolUI objectPoolUI;

    private void Update()
    {
        bullet3Timer += Time.deltaTime;
        bullet2Timer += Time.deltaTime;
        bullet1Timer += Time.deltaTime;
        Fire();
    }

    void Fire()
    {
        //플레이 화면일 때에만 시작
        if (NestedScrollManager.instance.targetIndex == 2)
        {
            //총알이 각각의 쿨타임마다 발사
            if (bullet1Timer > bullet1CoolTime)
            {
                GameObject bullet1 = objectPoolUI.GetObj(1);
                bullet1.transform.position = bullet1Tr.transform.position + new Vector3(0, 0.8f);

                bullet1Timer = 0;
            }

            if (bullet2Timer > bullet2CoolTime)
            {
                GameObject bulletR2 = objectPoolUI.GetObj(8);
                bulletR2.transform.position = bulletR2Tr.transform.position + new Vector3(0.2f, 0.3f);

                GameObject bulletL2 = objectPoolUI.GetObj(8);
                bulletL2.transform.position = bulletL2Tr.transform.position + new Vector3(-0.2f, 0.3f);

                bullet2Timer = 0;
            }

            if (bullet3Timer > bullet3CoolTime)
            {
                GameObject bulletR3 = objectPoolUI.GetObj(12);
                bulletR3.transform.position = bulletR3Tr.transform.position + new Vector3(0, 0.5f);

                GameObject bulletL3 = objectPoolUI.GetObj(12);
                bulletL3.transform.position = bulletL3Tr.transform.position + new Vector3(0, 0.5f);

                bullet3Timer = 0;
            }
        }
    }
}
