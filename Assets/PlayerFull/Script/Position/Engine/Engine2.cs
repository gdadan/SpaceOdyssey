using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine2 : Engine
{
    protected override void OnEngine()
    {
        if (BulletsManager.instance.bulletIndexs[4] == 0)
        {
            EquipEngine();
        }
        else
        {
            OffEngine();
        }

        base.OnEngine();
    }
}
