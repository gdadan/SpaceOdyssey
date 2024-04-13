using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    
    public bool trans = false;

    void Awake()
    {
        instance = this; 
    }

   
}
