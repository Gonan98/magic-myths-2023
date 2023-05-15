using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text roomText;

    private RoomInfo info;

    public void SetUp(RoomInfo roomInfo)
    {
        info = roomInfo;
        roomText.text = roomInfo.Name + " " + roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(info);
    }
}
