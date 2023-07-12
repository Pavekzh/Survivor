using UnityEngine;

public class MenuBootstrap : MonoBehaviour
{
    [SerializeField] ChoosePlayer choosePlayer;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] MessageController messenger;
    [SerializeField] ChoosePlayerUI choosePlayerUI;
    [SerializeField] MainMenuUI mainMenuUI;

    private void Awake()
    {
        InitChoosePlayerUI();
        InitMainMenuUI();
    }

    private void InitMainMenuUI()
    {
        mainMenuUI.InitDependencies(sceneLoader,messenger);
    }

    private void InitChoosePlayerUI()
    {
        this.choosePlayerUI.InitDependencies(choosePlayer);
    }
}