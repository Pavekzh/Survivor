using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MainMenuUI:MonoBehaviourPunCallbacks
{
    [SerializeField] private Button createLobby;
    [SerializeField] private Button connect;
    [SerializeField] private Button exit;

    private MessageController messenger;
    private SceneLoader sceneLoader;

    public void InitDependencies(SceneLoader sceneLoader,MessageController messenger)
    {
        this.sceneLoader = sceneLoader;
        this.messenger = messenger;
    }

    private void Start()
    {
        PhotonNetwork.NickName = "Player" + UnityEngine.Random.Range(1000, 9999);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();

        createLobby.onClick.AddListener(CreateLobby);
        connect.onClick.AddListener(Connect);
        exit.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void Connect()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    private void CreateLobby()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions() { MaxPlayers = 2 });

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        messenger.ShowMessage("Error: " + returnCode, message);
    }

    public override void OnJoinedRoom()
    {
        messenger.ShowMessage("Succes", "Player joined room");
        sceneLoader.LoadGameOnline();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        messenger.ShowMessage("Enter", newPlayer.NickName + " entered room");
    }

}

