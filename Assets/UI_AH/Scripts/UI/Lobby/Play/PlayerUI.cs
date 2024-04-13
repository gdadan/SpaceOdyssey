using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] float bullet1CoolTime; //ÃÑ¾ËC ÄðÅ¸ÀÓ
    [SerializeField] float bullet2CoolTime; //ÃÑ¾ËB ÄðÅ¸ÀÓ
    [SerializeField] float bullet3CoolTime; //ÃÑ¾ËA ÄðÅ¸ÀÓ
    
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
        //ÇÃ·¹ÀÌ È­¸éÀÏ ¶§¿¡¸¸ ½ÃÀÛ
        if (NestedScrollManager.instance.targetIndex == 2)
        {
            //ÃÑ¾ËÀÌ °¢°¢ÀÇ ÄðÅ¸ÀÓ¸¶´Ù ¹ß»ç
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
