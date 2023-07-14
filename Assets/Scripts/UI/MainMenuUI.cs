using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI:MonoBehaviour
{
    [SerializeField] Button createLobby;
    [SerializeField] Button connect;
    [SerializeField] Button exit;
    [SerializeField] MessageController messenger;
    [SerializeField] FusionConnect connector;

    private SceneLoader sceneLoader;

    public void InitDependencies(SceneLoader sceneLoader)
    {
        this.sceneLoader = sceneLoader;
    }

    private void Start()
    {
        createLobby.onClick.AddListener(CreateLobby);
        connect.onClick.AddListener(Connect);
        exit.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }

    private async void Connect()
    {                
        messenger.ShowMessage("", "Connecting...");
        await connector.Connect();        

    }

    private async void CreateLobby()
    {        
        messenger.ShowMessage("", "Connecting...");
        await connector.Connect();

    }

}

