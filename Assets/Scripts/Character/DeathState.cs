using System;
using UnityEngine;

public class DeathState : CharacterState
{
    public DeathState(Character character, StateMachine<CharacterState> stateMachine) : base(character, stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Death enter");
    }

    public override void Exit()
    {
        Debug.Log("Death exit");
    }

    public override bool IsAlive()
    {
        return false;
    }

    public override void HandleMoveInput(Vector2 input)
    {
        throw new NotImplementedException();
    }

    public override void TriggerEnter(Collider2D trigger) { }
    public override void HandleAttackInput(Vector2 input) { }

}

