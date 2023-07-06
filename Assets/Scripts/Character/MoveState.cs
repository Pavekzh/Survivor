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
        character.transform.Translate(direction * character.MoveSpeed * Time.deltaTime);
    }
}
