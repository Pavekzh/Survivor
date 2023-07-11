using UnityEngine;

public class MenuBootstrap : MonoBehaviour
{
    [SerializeField] ChoosePlayer choosePlayer;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] ChoosePlayerUI choosePlayerUI;
    [SerializeField] MainMenuUI mainMenuUI;

    private void Awake()
    {
        InitChoosePlayerUI();
        InitMainMenuUI();
    }

    private void InitMainMenuUI()
    {
        mainMenuUI.InitDependencies(sceneLoader);
    }

    private void InitChoosePlayerUI()
    {
        this.choosePlayerUI.InitDependencies(choosePlayer);
    }
}