using System;
using UnityEngine;

class MockInputDetector : InputDetector
{
    public override Vector2 GetAttackInput()
    {
        return Vector2.zero;
    }

    public override Vector2 GetMoveInput()
    {
        return Vector2.zero;
    }
}
