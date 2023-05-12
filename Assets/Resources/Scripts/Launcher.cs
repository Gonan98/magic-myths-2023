using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    #region Private Serializable Fields



    #endregion

    #region Private Fields

    private string gameVersion = "0.1";
    private byte maxPlayersPerRoom = 2;

    #endregion

    #region MonoBehaviour Callbacks

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
    }

    void Update()
    {

    }

    #endregion

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master.");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("UserName");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning("Disconnected from server: Cause " + cause);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        
    }

    #endregion

    #region Public Methods

    public void SetNickName(InputField nickName)
    {
        PhotonNetwork.NickName = nickName.text;
        MenuManager.Instance.OpenMenu("Main");
    }

    public void CreateRoom(InputField roomName)
    {
        if (string.IsNullOrEmpty(roomName.text)) return;
        PhotonNetwork.CreateRoom(roomName.text, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        MenuManager.Instance.OpenMenu("Loading");
    }

    public void OnClickPanel(string menu)
    {
        MenuManager.Instance.OpenMenu(menu);
    }

    public void OnClickBack()
    {
        MenuManager.Instance.OpenMenu("Main");
    }

    #endregion
}
