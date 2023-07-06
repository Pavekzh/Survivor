using UnityEngine;

public class MoveState : AliveState
{
    public MoveState(Character character, StateMachine<CharacterState> stateMachine) : base(character, stateMachine) { }

    public override void Enter()
    {
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
        Vector2 newPosition = (Vector2)(character.transform.position) + (direction * character.MoveSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(newPosition.x, character.MoveBoundaries.bounds.min.x + character.ColliderSize.x, character.MoveBoundaries.bounds.max.x - character.ColliderSize.x);
        float clampedY = Mathf.Clamp(newPosition.y, character.MoveBoundaries.bounds.min.y + character.ColliderSize.y, character.MoveBoundaries.bounds.max.y - character.ColliderSize.y);

        character.transform.position = new Vector3(clampedX, clampedY, character.transform.position.z);
    }
}
