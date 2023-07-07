using System;
using UnityEngine;

public class InputSelector:InputDetector
{
    [SerializeField] private AxesInputDetector axesInputDetector;
    [SerializeField] private JoysticksInputDetector joysticksInputDetector;

    private InputDetector selectedDetector;

    public override Vector2 GetAttackInput()
    {
        return selectedDetector.GetAttackInput();
    }

    public override Vector2 GetMoveInput()
    {
        return selectedDetector.GetMoveInput();
    }

    private void Start()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        selectedDetector = axesInputDetector;
        joysticksInputDetector.Disable();
#elif UNITY_ANDROID || UNITY_IOS 
        selectedDetector = joysticksInputDetector;
        joysticksInputDetector.Enable();
#endif
    }


}

