using UnityEngine;

public class Detector : MonoBehaviour
{
    // Player�� �浹 ���� �� �θ�(������ Enemy)�� ���¸� Attack���� �ٲ� 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && transform.position.y < 2.3f)
        {
            transform.parent.gameObject.GetComponent<Enemy>().state = Enemy.State.Attack;
            gameObject.SetActive(false);
        }
    }
}
