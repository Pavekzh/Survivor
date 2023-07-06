using System;
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

    public override void HandleInput(InputDetector inputDetector)
    {
        Vector2 moveInput = inputDetector.GetMoveInput();

        Move(moveInput);

        if (moveInput.magnitude == 0)
            stateMachine.ChangeState(character.IdleState);
    }

    public override void LogicUpdate()
    {
        throw new NotImplementedException();
    }

    public override void PhysicsUpdate()
    {
        throw new NotImplementedException();
    }
}
