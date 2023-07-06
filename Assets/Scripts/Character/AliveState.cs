using System;
using System.Collections;
using UnityEngine;

public abstract class AliveState:CharacterState
{
    public AliveState(Character character, StateMachine<CharacterState> stateMachine) : base(character, stateMachine) { }

    public override bool IsAlive()
    {
        return true;
    }

    public override void TriggerEnter(Collider trigger)
    {
        throw new NotImplementedException();
    }

    public override void HandleAttackInput(Vector2 input)
    {
        if (input.magnitude == 0)
            character.Weapon.StopAttack();
        else
        {        
            character.Weapon.AttackDirection = input;
            
            if (character.Weapon.IsAttacking == false)
                character.Weapon.StartAttack();
        }

    }
}

