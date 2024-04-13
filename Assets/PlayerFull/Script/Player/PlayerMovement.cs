using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        CameraMovement();
        Movement();
    }

    void CameraMovement()
    {
        //화면 밖으로 플레이어가 못나가게 막음
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0.05f) pos.x = 0.05f;
        if (pos.x > 0.95f) pos.x = 0.95f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 0.9f) pos.y = 0.9f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 vec = new Vector3(x, y, 0);
        vec.Normalize();

        transform.position += vec * moveSpeed * Time.deltaTime;
    }
}
