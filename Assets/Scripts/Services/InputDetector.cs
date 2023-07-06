using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputDetector : MonoBehaviour
{
    public abstract Vector2 GetMoveInput();
    public abstract Vector2 GetAttackInput();
}
