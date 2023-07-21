using UnityEngine;
using UnityEngine.EventSystems;

public class AxesInputDetector : InputDetector
{
    [SerializeField] private string forwardMoveAxis = "Vertical";
    [SerializeField] private string sideMoveAxis = "Horizontal";
    [SerializeField] private string attackAxis = "Fire1";

    private Character character;
    private Camera camera;

    public void InitDepenpencies(Character character,Camera camera)
    {
        this.character = character;
        this.camera = camera;
    }

    public override Vector2 GetAttackInput()
    {
        
        Vector2 input = Vector2.zero;

        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetAxis(attackAxis) != 0)
            input = camera.ScreenToWorldPoint(Input.mousePosition) - character.transform.position;

        return input.normalized;
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
