using TMPro;
using UnityEngine;
using System;

public class WaveSystemUI:MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text waveStateText;

    private WaveSystem waveSystem;

    public void InitDependencies(WaveSystem waveSystem)
    {
        this.waveSystem = waveSystem;
        this.waveSystem.OnRestStarted += RestStarted;
        this.waveSystem.OnSpawnStarted += WaveStarted;
        this.waveSystem.OnTimerChanged += TimerChanged;
    }

    private void TimerChanged(float time)
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

