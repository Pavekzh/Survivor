using UnityEngine;

public class MoveState : CharacterAliveState
{
    public MoveState(Character character, StateMachine<CharacterState> stateMachine) : base(character, stateMachine) { }

    public override void Enter()
    {
        if (character.HasStateAuthority)
            character.RPC_AnimateMove();
        Debug.Log("Move enter");
    }

    public override void Exit()
    {
        Debug.Log("Move exit");
    }

    public override void HandleMoveInput(Vector2 input)
    {
        Move(input);

        if (input.magnitude == 0)
            stateMachine.ChangeState(character.IdleState);
    }

    protected void Move(Vector2 direction)
    {
        SetLookDirection(direction);

        Vector2 newPosition = (Vector2)(character.transform.position) + (direction * character.MoveSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(newPosition.x, character.MoveBoundaries.min.x + character.ColliderSize.x, character.MoveBoundaries.max.x - character.ColliderSize.x);
        float clampedY = Mathf.Clamp(newPosition.y, character.MoveBoundaries.min.y + character.ColliderSize.y, character.MoveBoundaries.max.y - character.ColliderSize.y);

        character.transform.position = new Vector3(clampedX, clampedY, character.transform.position.z);
    }
}
