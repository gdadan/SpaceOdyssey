using UnityEngine;

public class Detector : MonoBehaviour
{
    // Player와 충돌 했을 때 부모(장착한 Enemy)의 상태를 Attack으로 바꿈 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && transform.position.y < 2.3f)
        {
            transform.parent.gameObject.GetComponent<Enemy>().state = Enemy.State.Attack;
            gameObject.SetActive(false);
        }
    }
}
