using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] float maxHealth = 100f;
    float playerSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 movePos = new Vector3(x, y, 0);

        transform.position += movePos * Time.deltaTime * playerSpeed;
    }
}
