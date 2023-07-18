﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion.Sockets;
using Fusion;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

public class FusionConnect : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkRunner runner;
    [SerializeField] private NetworkSceneManagerDefault sceneManager;
    [SerializeField] private string roomName;
    [SerializeField] private int gameScene;


    [SerializeField] private Investigator investigator;

    public const int PlayersCount = 2;

    public async Task Connect()
    {
        StartGameArgs args = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = roomName,
            Scene = gameScene,
            PlayerCount = FusionConnect.PlayersCount,
            SceneManager = sceneManager
        };
        await runner.StartGame(args);
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        if (runner.IsSharedModeMasterClient)
            runner.Spawn(investigator);
        Debug.Log("SceneLoadDone");
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Joined player " + player.PlayerId);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Left player " + player.PlayerId);
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("Connected to server");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.LogWarning("Disconnected from server");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.LogWarning("Connection failed");
    }

    #region unused Fusion callbacks
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }
    #endregion
}
