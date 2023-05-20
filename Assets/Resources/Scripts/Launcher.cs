using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    #region Private Serializable Fields

    [SerializeField] private Text RoomNameText;
    [SerializeField] private Transform RoomListContent;
    [SerializeField] private GameObject RoomListItemPrefab;
    [SerializeField] private Transform PlayerListContent;
    [SerializeField] private GameObject PlayerListItemPrefab;
    [SerializeField] private GameObject StartGameButton;

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
        PhotonNetwork.AutomaticallySyncScene = true;
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
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to Lobby");
        if (string.IsNullOrEmpty(PhotonNetwork.NickName))
            MenuManager.Instance.OpenMenu("UserName");
        else
            MenuManager.Instance.OpenMenu("Main");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("Room");
        RoomNameText.text = "Sala: " + PhotonNetwork.CurrentRoom.Name;
        Player[] players = PhotonNetwork.PlayerList;

        foreach(Transform child in PlayerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning("Disconnected from server: Cause " + cause);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform t in RoomListContent)
        {
            Destroy(t.gameObject);
        }
        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList) continue;
            Instantiate(RoomListItemPrefab, RoomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("Main");
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

    public void JoinRoom(InputField roomName)
    {
        if (string.IsNullOrEmpty(roomName.text)) return;
        PhotonNetwork.JoinRoom(roomName.text);
        MenuManager.Instance.OpenMenu("Loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
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

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("Loading");
    }

    public void SetPlayerReady(bool ready)
    {

    }

    public void SetPlayerReady(Player player, bool ready)
    {
        
    }

    #endregion
}
