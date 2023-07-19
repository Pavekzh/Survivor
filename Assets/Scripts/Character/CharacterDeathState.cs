using System;
using UnityEngine;

public class CharacterDeathState : CharacterState
{
    public CharacterDeathState(Character character, StateMachine<CharacterState> stateMachine) : base(character, stateMachine) { }

    public override void Enter()
    {
        if (character.HasStateAuthority)
            character.RPC_AnimateDeath();
        Debug.Log("Death enter");
    }

    public override void Exit()
    {
        Debug.Log("Death exit");
    }

    public override bool IsAlive { get => false; }

    public override void HandleMoveInput(Vector2 input)
    {
        Debug.LogError("Player spectating not implemented");
    }

    public override void HandleDamage(float damage,string sender) { }
    public override void HandleAttackInput(Vector2 input) { }

}

