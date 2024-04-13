using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    float backGroundSpeed = 0.5f;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * backGroundSpeed);
        if (transform.position.y < -10)
        {
            transform.position = new Vector3(transform.position.x, 10.5f, transform.position.z);
        }
    }
}
