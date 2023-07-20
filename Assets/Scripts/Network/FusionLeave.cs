using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FusionLeave:MonoBehaviour,INetworkRunnerCallbacks
{
    private TargetDesignator targetDesignator;
    private NetworkRunner network;
    private LoseDetection loseDetection;
    private SceneLoader sceneLoader;
    private WaveSystem waveSystem;

    public void InitDependencies(NetworkRunner network, LoseDetection loseDetection, TargetDesignator targetDesignator, WaveSystem waveSystem, SceneLoader sceneLoader)
    {
        this.network = network;        

        this.sceneLoader = sceneLoader;
        this.loseDetection = loseDetection;
        this.targetDesignator = targetDesignator;
        this.waveSystem = waveSystem;
    }

    public async void Leave()
    {        
        await network.Shutdown();
        sceneLoader.LoadStartup();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        waveSystem.RemotePlayerLeave();
        loseDetection.RemotePlayerLeave();
        targetDesignator.RemovePlayer();
    }

    #region unused Fusion callbacks
    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }
    #endregion
}
