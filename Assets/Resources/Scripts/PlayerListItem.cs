using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
	[SerializeField] private Text NameText;
    [SerializeField] private Text PingText;
    [SerializeField] private Text ReadyText;
	Player player;

    void Awake()
    {

    }

    void Start()
    {
        
    }

	public void SetUp(Player _player)
	{
		player = _player;
		NameText.text = _player.NickName;
        PingText.text = PhotonNetwork.GetPing().ToString() + " ms";
	}

	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		if(player == otherPlayer)
		{
			Destroy(gameObject);
		}
	}

	public override void OnLeftRoom()
	{
		Destroy(gameObject);
	}
}
