using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    public GameObject startButton;
    public GameObject cancelButton;
    [SerializeField] string sceneToLoadNext;

    private void Awake()
    {
        if (lobby != null && lobby != this)
        {
            gameObject.SetActive(false);
        }
        else
        {
            lobby = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Player conected to Master");
        startButton.SetActive(true);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Player Joined the Room");
        ChangeScen(sceneToLoadNext);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room");
        CreateRoom();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create a new Room");
        CreateRoom();
    }
    //----------------------------------------------------------------------------------------------------
    void CreateRoom()
    {
        int randomRoom = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
        PhotonNetwork.CreateRoom("Room" + randomRoom, roomOps);
    }
    public void OnStartPresed()
    {
        startButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }
    public void OnCancelPresed()
    {
        startButton.SetActive(true);
        cancelButton.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
    public void ChangeScen(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
