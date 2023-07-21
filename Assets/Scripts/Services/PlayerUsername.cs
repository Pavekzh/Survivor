using UnityEngine;

public class PlayerUsername : MonoBehaviour
{
    [SerializeField] private string usernameKey = "Username";

    public string GetUsername()
    {
        return PlayerPrefs.GetString(usernameKey);
    }

    public void SetUsername(string username)
    {
        PlayerPrefs.SetString(usernameKey, username);
    }
}