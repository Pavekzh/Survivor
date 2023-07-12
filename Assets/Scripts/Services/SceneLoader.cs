using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private int gameScene;
    [SerializeField] private int startupScene;
    [SerializeField] private string gameScenName;

    public void LoadGameOnline()
    {
        if (loadingScreen != null)
            loadingScreen.SetActive(true);

        PhotonNetwork.LoadLevel(gameScenName);
    }

    public void LoadGame()
    {
        if(loadingScreen != null)
            loadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync(gameScene);
    }
    public void LoadStartup()
    {
        if(loadingScreen != null)
            loadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync(startupScene);
    }

}