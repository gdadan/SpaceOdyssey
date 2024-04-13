using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPopupManager : MonoBehaviour
{
    public ItemExplainUI itemExplainUI;

    public BulletSlot bulletSlot;

    public int cusItemKey;
    
    //������ �˾�â Open
    public virtual void OpenBulletPopUp(BulletSlot _bulletSlot)
    {
        bulletSlot = _bulletSlot;
        cusItemKey = bulletSlot.key;
        itemExplainUI.ButtonActive(bulletSlot.slotType);
        itemExplainUI.itemPopUp.SetActive(true);
        itemExplainUI.ItemExplain(cusItemKey);
    }
}
