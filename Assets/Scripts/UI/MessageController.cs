using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text message;
    [SerializeField] private Button okButton;

    private void Start()
    {
        okButton.onClick.AddListener(Close);
    }

    public void ShowMessage(string Title, string Message)
    {
        this.title.text = Title;
        this.message.text = Message;

        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}