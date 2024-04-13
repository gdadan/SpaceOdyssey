using UnityEngine;

public class Gold : MonoBehaviour
{
    public int gold;
    float goldSpeed = 1f;

    private void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * goldSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Invincibility"))
        {
            SoundManager.instance.PlaySFX(3);
            GoldManager.instance.playerGold += gold;
            gameObject.SetActive(false);
        }
    }
}
