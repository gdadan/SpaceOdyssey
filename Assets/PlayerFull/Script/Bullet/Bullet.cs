using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static Bullet instance;
    
    public float bulletSpeed = 10;

    public string synergy1;
    public string synergy2;
    public string synergy3;

    public float atk;
    public float def;

    public float engineAtk;
    public float engineDef;

    public float finalAtk;
    public float finalDef;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gameObject.tag = "Bullet";
        CalculateStat();        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * bulletSpeed * Time.deltaTime;
        //Debug.Log(finalAtk);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().health -= finalAtk;
            gameObject.SetActive(false);
        }
    }

    public void CalculateStat()
    {
        finalAtk = (atk + StatesManager.instance.skillAtk) * StatesManager.instance.synergyAtk;
        finalDef += StatesManager.instance.skillDef;
    }
}
