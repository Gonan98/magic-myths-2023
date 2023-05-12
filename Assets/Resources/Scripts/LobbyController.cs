using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject CreatePanel;
    [SerializeField] private GameObject JoinPanel;
    [SerializeField] private GameObject SearchPanel;
    [SerializeField] private GameObject CreateForm;
    [SerializeField] private GameObject JoinForm;
    [SerializeField] private GameObject MainRoomPanel;

    private byte maxPlayersPerRoom = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Se creó una sala");
    }

    public override void OnJoinedRoom()
    {
        TogglePanels(true);
        MainRoomPanel.SetActive(true);
        var roomTitle = MainRoomPanel.GetComponentInChildren<Text>();
        roomTitle.text = PhotonNetwork.CurrentRoom.Name;
    }

    public void OnClickCreatePanel()
    {
        CreateForm.SetActive(true);
        TogglePanels(false);
    }

    public void OnClickJoinPanel()
    {
        JoinForm.SetActive(true);
        TogglePanels(false);
    }

    public void TogglePanels(bool value)
    {
        CreatePanel.SetActive(value);
        JoinPanel.SetActive(value);
        SearchPanel.SetActive(value);
    }

    public void OnClickCreateButton()
    {
        string roomName = CreateForm.GetComponentInChildren<InputField>().text;
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public void OnClickBackFormButton(GameObject form)
    {
        form.SetActive(false);
        TogglePanels(true);
    }
}
