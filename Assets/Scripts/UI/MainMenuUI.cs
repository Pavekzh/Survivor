using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI:MonoBehaviour
{
    [SerializeField] Button createLobby;
    [SerializeField] Button connect;
    [SerializeField] Button exit;

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

    private void Connect()
    {
        sceneLoader.LoadGame();
        throw new NotImplementedException();
    }

    private void CreateLobby()
    {
        sceneLoader.LoadGame();
        throw new NotImplementedException();
    }

}

