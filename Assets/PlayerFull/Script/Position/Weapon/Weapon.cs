using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour // �Ѿ� ����(�ش� ��ġ��)
{
    float timer; //�ð�
    protected void WeaponBullet(List<Bullet> bullet, int? bulletIndex, int coolTimeMod) //�Ѿ� ����
    {
        timer += Time.deltaTime; //�ð��� �귯��.
        if (timer >= (StatesManager.instance.Speed / coolTimeMod) && bulletIndex != null) // �ð��� cooltime���� ũ�ų� ������ ����
        {
            if (bullet == BulletsManager.instance.bullet2)
            {
                SoundManager.instance.PlaySFX2(0);
            }
            Bullet b = Instantiate(bullet[(int)bulletIndex], BulletsManager.instance.gameObject.transform); //bullet[(int)bulletIndex] -> bullet[������ bulletIndex] -> bulletIndex : �Ѿ� ����
            b.transform.position = transform.position;
            timer = 0; //�ð� �ʱ�ȭ
        }
    }
}

