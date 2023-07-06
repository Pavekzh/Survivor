using System;
using UnityEngine;

public class IdleState : AliveState
{
    public IdleState(Character character, StateMachine<CharacterState> stateMachine) : base(character, stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Idle enter");
    }

    public override void Exit()
    {
        Debug.Log("Idle exit");
    }

    public override void HandleInput(InputDetector inputDetector)
    {
        Vector2 moveInput = inputDetector.GetMoveInput();

        if (moveInput.magnitude > 0)
            stateMachine.ChangeState(character.MoveState);
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
