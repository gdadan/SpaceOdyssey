using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour // 총알 생성(해당 위치에)
{
    float timer; //시간
    protected void WeaponBullet(List<Bullet> bullet, int? bulletIndex, int coolTimeMod) //총알 생성
    {
        timer += Time.deltaTime; //시간이 흘러감.
        if (timer >= (StatesManager.instance.Speed / coolTimeMod) && bulletIndex != null) // 시간이 cooltime보다 크거나 같으면 실행
        {
            if (bullet == BulletsManager.instance.bullet2)
            {
                SoundManager.instance.PlaySFX2(0);
            }
            Bullet b = Instantiate(bullet[(int)bulletIndex], BulletsManager.instance.gameObject.transform); //bullet[(int)bulletIndex] -> bullet[정수형 bulletIndex] -> bulletIndex : 총알 종류
            b.transform.position = transform.position;
            timer = 0; //시간 초기화
        }
    }
}

