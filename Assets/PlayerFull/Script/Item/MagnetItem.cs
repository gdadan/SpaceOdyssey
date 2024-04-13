using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetItem : MonoBehaviour
{
    public PlayerMovement magnet;
    public float speed;
  
    private void Start()
    {
        magnet = GameManager.instance.player;
    }

    void FixedUpdate()
    {
        Magnet();      
    }
    
    void Magnet()
    {
        if(ItemManager.instance.trans == true)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, magnet.transform.position, Time.deltaTime * speed * 3.0f);
            transform.position = pos;
        }     
    }
}
