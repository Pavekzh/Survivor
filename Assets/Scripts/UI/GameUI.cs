using UnityEngine;
using UnityEngine.UI;

public class GameUI:MonoBehaviour
{
    [SerializeField] private Button leaveButton;
    [SerializeField] private TMPro.TMP_Text killsText;
    [SerializeField] private TMPro.TMP_Text damageText;

    private FusionLeave fusionLeave;
    private ScoreCounter scoreCounter;
    private Character character;

    public void InitDependencies(FusionLeave fusionLeave,Character character, ScoreCounter scoreCounter)
    {
        this.fusionLeave = fusionLeave;
        this.scoreCounter = scoreCounter;
        this.character = character;

        scoreCounter.OnRecordsChanged += ScoreChanged;
        leaveButton.onClick.AddListener(fusionLeave.Leave);
    }

    private void ScoreChanged(string sender)
    {
        if(sender == character.ID)
        {
            damageText.text = scoreCounter.Records[sender].Damage.ToString();
            killsText.text = scoreCounter.Records[sender].Kills.ToString();
        }
    }
}