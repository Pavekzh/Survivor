using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConnectUI:MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private Button startButton;
    [SerializeField] private int usernameMinLength = 3;

    private bool isConnecting;

    private PlayerUsername playerUsername;
    private FusionConnect fusionConnect;
    private MessageController messenger;

    public void InitDependencies(PlayerUsername playerUsername,FusionConnect fusionConnect,MessageController messenger)
    {
        this.playerUsername = playerUsername;
        this.fusionConnect = fusionConnect;
        this.messenger = messenger;

        this.startButton.onClick.AddListener(StartGame);
    }

    public void Open()
    {
        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    private async void StartGame()
    {
        if(usernameInput.text.Length < usernameMinLength)
        {
            messenger.ShowMessage("Error", "Username must be at least 3 symbols");
            return;
        }
        if (isConnecting)
            return;

        isConnecting = true;
        messenger.ShowMessage("", "Connecting...", true);
        playerUsername.SetUsername(usernameInput.text);


        await fusionConnect.Connect(ConnectFailed);
    }

    private void ConnectFailed(string message)
    {
        isConnecting = false;
        messenger.ShowMessage("Error", message);
    }
    
}