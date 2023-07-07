using System;
using UnityEngine;

public class JoysticksInputDetector : InputDetector
{
    [SerializeField] FixedJoystick moveJoystick;
    [SerializeField] FixedJoystick attackJoystick;

    public void Disable()
    {
        moveJoystick.gameObject.SetActive(false);
        attackJoystick.gameObject.SetActive(false);
    }

    public void Enable()
    {
        moveJoystick.gameObject.SetActive(true);
        attackJoystick.gameObject.SetActive(true);
    }

    public override Vector2 GetAttackInput()
    {
        Vector2 input = attackJoystick.Direction;
        return input.normalized;
    }

    public override Vector2 GetMoveInput()
    {
        Vector2 input = moveJoystick.Direction;
        return input;
    }
}

