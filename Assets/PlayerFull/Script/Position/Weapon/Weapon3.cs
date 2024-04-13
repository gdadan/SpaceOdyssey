using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3 : Weapon
{
    void Update()
    {
        WeaponBullet(BulletsManager.instance.bullet3, BulletsManager.instance.bulletIndexs[2], 5);
    }
}
