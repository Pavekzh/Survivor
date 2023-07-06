using System;
using UnityEngine;

class GameBootstrap:MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private InputDetector inputDetector;
    [SerializeField] private AxesInputDetector axesInputDetector;
    [SerializeField] private Camera camera;
    [Header("Character")]
    [SerializeField] private Character character;

    private void Awake()
    {
        InitCharacter();
        InitAxesInput();
    }

    private void InitAxesInput()
    {
        axesInputDetector.InitDepenpencies(character, camera);
    }

    private void InitCharacter()
    {
        character.InitDependencies(inputDetector);
    }

}

