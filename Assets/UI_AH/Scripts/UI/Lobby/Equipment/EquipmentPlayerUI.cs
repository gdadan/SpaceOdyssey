using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EquipmentPlayerUI : MonoBehaviour
{
    //장착하고 있는 총알이 발사되는 스크립트
    public ObjectPoolUI objectPoolUI;

    [SerializeField]
    float bulletCoolTime; //총알 쿨타임

    float bulletTimer;

    public int key;

    public Transform bullet1Tr;
    public Transform bulletR2Tr;
    public Transform bulletR3Tr;
    public Transform bulletL2Tr;
    public Transform bulletL3Tr;

    void Update()
    {
        bulletTimer += Time.deltaTime;

        FireBullet();
    }

    void FireBullet()
    {
        if (NestedScrollManager.instance.targetIndex != 1)
            return;

        if (InventoryManager.instance.equipSlot == null)
            return;

        if (bulletTimer > bulletCoolTime)
        {
            key = DataManager.instance.inGameData.ig_playerBulletData.bulletKey;

            if (TsvLoader.instance.GetString(key, "type") == "bullet1")
            {
                GameObject bullet1 = objectPoolUI.GetObj(key - 1);
                bullet1.transform.position = bullet1Tr.transform.position + new Vector3(0, 0.8f);
            }
            else if (TsvLoader.instance.GetString(key, "type") == "bullet2")
            {
                GameObject bulletR2 = objectPoolUI.GetObj(key - 1);
                bulletR2.transform.position = bulletR2Tr.transform.position + new Vector3(0.15f, 0.4f);

                GameObject bulletL2 = objectPoolUI.GetObj(key - 1);
                bulletL2.transform.position = bulletL2Tr.transform.position + new Vector3(-0.15f, 0.4f);

            }
            else if (TsvLoader.instance.GetString(key, "type") == "bullet3")
            {
                GameObject bulletR3 = objectPoolUI.GetObj(key - 1);
                bulletR3.transform.position = bulletR3Tr.transform.position + new Vector3(0, 0.6f);

                GameObject bulletL3 = objectPoolUI.GetObj(key - 1);
                bulletL3.transform.position = bulletL3Tr.transform.position + new Vector3(0, 0.6f);
            }

            bulletTimer = 0;
        }
    }
}
