using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;

    public Gold goldPrefab;

    public TextMeshProUGUI goldText;

    public int playerGold;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        goldText.text = string.Format("<sprite=0> {0}", playerGold);
    }
}
