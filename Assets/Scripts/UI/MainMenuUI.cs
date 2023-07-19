using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI:MonoBehaviour
{
    [SerializeField] Button play;
    [SerializeField] Button exit;

    private ConnectUI connectUI;

    public void InitDependencies(ConnectUI connectUI)
    {
        this.connectUI = connectUI;
    }

    private void Start()
    {
        play.onClick.AddListener(Play);
        exit.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void Play()
    {
        connectUI.Open();
    }


}

