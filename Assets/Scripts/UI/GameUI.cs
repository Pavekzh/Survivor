using UnityEngine;
using UnityEngine.UI;

public class GameUI:MonoBehaviour
{
    [SerializeField] private Button leaveButton;

    private FusionLeave fusionLeave;

    public void InitDependencies(FusionLeave fusionLeave)
    {
        this.fusionLeave = fusionLeave;

        leaveButton.onClick.AddListener(fusionLeave.Leave);
    }


}