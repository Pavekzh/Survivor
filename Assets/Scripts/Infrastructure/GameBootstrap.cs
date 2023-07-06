using System;
using UnityEngine;

class GameBootstrap:MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private InputDetector inputDetector;
    [SerializeField] private AxesInputDetector axesInputDetector;
    [SerializeField] private Camera camera;
    [SerializeField] private CameraFollow cameraFollow;    
    [SerializeField] private BoxCollider2D moveBoundaries;
    [Header("Character")]
    [SerializeField] private Character character;


    private void Awake()
    {
        InitCharacter();
        InitAxesInput();
        InitCameraFollow();
    }

    private void InitAxesInput()
    {
        axesInputDetector.InitDepenpencies(character, camera);
    }

    private void InitCameraFollow()
    {
        cameraFollow.InitDependencies(moveBoundaries);
    }

    private void InitCharacter()
    {
        character.InitDependencies(inputDetector,moveBoundaries);
    }

}

