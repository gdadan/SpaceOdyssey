using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    //로비로 가는 스크립트
    public void ToLobby()
    {
        SoundManager.instance.PlaySFX(0);

        //start 버튼을 누르면 로딩 화면에서 게임 플레이 화면으로
        LoadingSceneController.LoadScene("LobbyScene");
    }

    public void AddGold()
    {
        DataManager.instance.playerData.gold += GoldManager.instance.playerGold;
    }
}
