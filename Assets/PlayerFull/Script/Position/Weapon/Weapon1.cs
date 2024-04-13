using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : Weapon
{
    void Update()
    {
        WeaponBullet(BulletsManager.instance.bullet1, BulletsManager.instance.bulletIndexs[0], 1);
    }
}
