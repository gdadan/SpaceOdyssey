using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerMovement player;
    public Transform enemyBulletParents;

    private void Awake()
    {
        instance = this;

        Time.timeScale = 1;
        SoundManager.instance.BGMStart(1);
    }
}
