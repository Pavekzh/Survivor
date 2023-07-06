using System;
using UnityEngine;

public class KeyboardInputDetector : InputDetector
{
    [SerializeField] private string forwardMoveAxis = "Vertical";
    [SerializeField] private string sideMoveAxis = "Horizontal";

    public override Vector2 GetAttackInput()
    {
        throw new NotImplementedException();
    }

    public override Vector2 GetMoveInput()
    {
        Vector2 input = Vector2.zero;

        float forward = Input.GetAxis(forwardMoveAxis);
        float side = Input.GetAxis(sideMoveAxis);

        if (forward != 0)
            input.y = forward;
        if (side != 0)
            input.x = side;

        if (input.magnitude > 1)
            input.Normalize();

        return input;
    }
}
