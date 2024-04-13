using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2 : Weapon
{
    void Update()
    {
        WeaponBullet(BulletsManager.instance.bullet2, BulletsManager.instance.bulletIndexs[1], 3);
    }
}
    

