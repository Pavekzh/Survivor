using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WaveSystemUI:MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text waveStateText;
    [SerializeField] private GameObject startGamePanel;
    [SerializeField] private TMP_Text readyPlayersText;
    [Header("Ready button")]
    [SerializeField] private Button readybutton;
    [SerializeField] private Sprite unreadySprite;
    [SerializeField] private Sprite readySprite;

    private WaveSystem waveSystem;

    private Image readyButtonImage;

    public void InitDependencies(WaveSystem waveSystem)
    {
        this.waveSystem = waveSystem;        
        this.waveSystem.OnPlayersReadyChanged += UpdateReadyPlayers;
        this.waveSystem.OnGameStarted += GameStarted;
        this.waveSystem.OnRestStarted += RestStarted;
        this.waveSystem.OnSpawnStarted += WaveStarted;
        this.waveSystem.OnTimerChanged += UpdateTimer;

        readybutton.onClick.AddListener(ReadyChange);
        readyButtonImage = readybutton.GetComponent<Image>();
        readyButtonImage.sprite = unreadySprite;
    }

    private void GameStarted()
    {
        startGamePanel.SetActive(false);
    }

    private void ReadyChange()
    {
        waveSystem.ChangeReadyState();

        if (waveSystem.IsReady)
            readyButtonImage.sprite = readySprite;
        else
            readyButtonImage.sprite = unreadySprite;
    }

    private void UpdateReadyPlayers(int ready)
    {
        readyPlayersText.text = ready.ToString() + "/" + FusionConnect.PlayersCount;
    }

    private void UpdateTimer(float time)
    {
        DateTime dateTime = new DateTime().AddSeconds(time);

        timerText.text = dateTime.ToString("mm:ss");
    }

    private void WaveStarted()
    {
        waveStateText.text = "Wave";
    }

    private void RestStarted()
    {
        waveStateText.text = "Rest";
    }
}

