using System;
using UnityEngine;

class GameBootstrap:MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private InputDetector inputDetector;
    [Header("Character")]
    [SerializeField] private Character character;

    private void Awake()
    {
        character.InitDependencies(inputDetector);
    }
}

