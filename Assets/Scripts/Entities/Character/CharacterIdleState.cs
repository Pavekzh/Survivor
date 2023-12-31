﻿using UnityEngine;

public class CharacterIdleState : CharacterAliveState
{
    public CharacterIdleState(Character character, StateMachine<CharacterState> stateMachine) : base(character, stateMachine) { }

    public override void Enter()
    {
        if (character.HasStateAuthority)
            character.RPC_AnimateIdle();
        Debug.Log("Idle enter");
    }

    public override void Exit()
    {
        Debug.Log("Idle exit");
    }

    public override void HandleMoveInput(Vector2 input)
    {
        if (input.magnitude > 0)
            stateMachine.SwitchState<CharacterMoveState>();
    }
}
