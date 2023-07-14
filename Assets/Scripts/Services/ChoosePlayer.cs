using System;
using UnityEngine;

public class ChoosePlayer : MonoBehaviour
{
    [SerializeField] private Character[] characters;
    [SerializeField] private string choosedIndexKey = "ChoosedCharacter";

    private int? choosed;

    public int? Choosed
    {
        get
        {
            if (choosed == null)
                choosed = PlayerPrefs.GetInt(choosedIndexKey);

            return choosed;
        }
    }


    public Character GetChoosed()
    {
        return characters[Choosed.Value];
    }

    public void Choose(int index)
    {
        choosed = index;
        PlayerPrefs.SetInt(choosedIndexKey, choosed.Value);
    }
}


