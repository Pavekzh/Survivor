using System;
using UnityEngine;

public class DeathState : CharacterState
{
    public DeathState(Character character, StateMachine<CharacterState> stateMachine) : base(character, stateMachine) { }

    public override void Enter()
    {
        throw new NotImplementedException();
    }

    public override void Exit()
    {
        throw new NotImplementedException();
    }

    public override bool IsAlive()
    {
        return false;
    }

    public override void HandleMoveInput(Vector2 input)
    {
        throw new NotImplementedException();
    }

    public override void TriggerEnter(Collider trigger) { }
    public override void HandleAttackInput(Vector2 input) { }

}

