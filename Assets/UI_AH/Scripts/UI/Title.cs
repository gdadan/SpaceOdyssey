using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    //�κ�� ���� ��ũ��Ʈ
    public void ToLobby()
    {
        SoundManager.instance.PlaySFX(0);

        //start ��ư�� ������ �ε� ȭ�鿡�� ���� �÷��� ȭ������
        LoadingSceneController.LoadScene("LobbyScene");
    }

    public void AddGold()
    {
        DataManager.instance.playerData.gold += GoldManager.instance.playerGold;
    }
}
