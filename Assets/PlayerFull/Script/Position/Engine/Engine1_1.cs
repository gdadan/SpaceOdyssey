using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine1_1 : Engine
{
    protected override void OnEngine()
    {
        if (BulletsManager.instance.bulletIndexs[3] == 0)
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
