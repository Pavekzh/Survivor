using UnityEngine;

public class MenuBootstrap : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private PlayerUsername playerUsername;
    [SerializeField] private ChoosePlayer choosePlayer;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private FusionConnect connect;

    [Header("UI")]
    [SerializeField] private MessageController messenger;
    [SerializeField] private ConnectUI connectUI;
    [SerializeField] private ChoosePlayerUI choosePlayerUI;
    [SerializeField] private MainMenuUI mainMenuUI;

    private void Awake()
    {
        InitChoosePlayerUI();
        InitMainMenuUI();
        InitConnectUI();
    }

    private void InitConnectUI()
    {
        connectUI.InitDependencies(playerUsername, connect, messenger);
    }

    private void InitMainMenuUI()
    {
        mainMenuUI.InitDependencies(connectUI);
    }

    private void InitChoosePlayerUI()
    {
        this.choosePlayerUI.InitDependencies(choosePlayer);
    }
}