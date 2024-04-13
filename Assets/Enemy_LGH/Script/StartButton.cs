using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void Gamestart()
    {
        StageManager.instance.stageEnd = true;
    }
}
