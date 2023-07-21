using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Collections.Generic;

public class GameOverUI:MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text title;
    [SerializeField] private Button menu;
    [Header("First player")]
    [SerializeField] private TMP_Text username1;
    [SerializeField] private TMP_Text kills1;
    [SerializeField] private TMP_Text damage1;
    [Header("Second player")]
    [SerializeField] private TMP_Text username2;
    [SerializeField] private TMP_Text kills2;
    [SerializeField] private TMP_Text damage2;

    private const string WinMessage = "You survived!";
    private const string LoseMessage = "You died!";

    private ScoreCounter scoreCounter;
    private FusionLeave fusionLeave;

    public void InitDependencies(ScoreCounter scoreCounter,FusionLeave fusionLeave)
    {
        this.scoreCounter = scoreCounter;
        this.fusionLeave = fusionLeave;

        menu.onClick.AddListener(Menu);
    }

    public void OpenWin()
    {
        Open(WinMessage);
    }

    public void OpenLose()
    {
        Open(LoseMessage);
    }

    private void Open(string title)
    {
        panel.SetActive(true);
        this.title.text = title;

        if (scoreCounter.Records.Count < 1)
            return;
        KeyValuePair<string,ScoreCounter.ScoreRecord> record1 = scoreCounter.Records.ElementAt(0);
        username1.text = record1.Key;
        kills1.text = record1.Value.Kills.ToString();
        damage1.text = record1.Value.Damage.ToString();

        if (scoreCounter.Records.Count < 2)
            return;
        KeyValuePair<string, ScoreCounter.ScoreRecord> record2 = scoreCounter.Records.ElementAt(1);
        username2.text = record2.Key;
        kills2.text = record2.Value.Kills.ToString();
        damage2.text = record2.Value.Damage.ToString();
    }

    private void Menu()
    {
        fusionLeave.Leave();
    }
}