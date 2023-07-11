using System;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePlayerUI:MonoBehaviour
{
    [SerializeField] private Button[] characterButtons;
    [SerializeField] private Color selectedColor;

    private Image[] characterImages;

    private ChoosePlayer choosePlayer;

    public void InitDependencies(ChoosePlayer choosePlayer)
    {
        this.choosePlayer = choosePlayer;
    }

    private void Start()
    {
        characterImages = new Image[characterButtons.Length];

        for(int i = 0; i < characterButtons.Length; i++)
        {
            int index = i;
            characterImages[i] = characterButtons[i].GetComponent<Image>();
            characterButtons[i].onClick.AddListener(() => ChooseCharacter(index));
        }

        characterImages[choosePlayer.Choosed.Value].color = selectedColor;
    }

    private void ChooseCharacter(int index)
    {
        choosePlayer.Choose(index);
        foreach(Image image in characterImages)
        {
            image.color = Color.white;
        }

        characterImages[index].color = selectedColor;
    }
}

