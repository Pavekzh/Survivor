using System;
using System.Collections;
using UnityEngine;

public abstract class CharacterAliveState:CharacterState
{
    public CharacterAliveState(Character character, StateMachine<CharacterState> stateMachine) : base(character, stateMachine) { }

    public override bool IsAlive { get => true; }

    public override void HandleTakeDamage()
    {
        if (character.Health.CurrentHealth == 0)
        {
            character.Weapon.StopAttack();
            stateMachine.ChangeState(character.DeathState);
        }

    }

    public override void HandleAttackInput(Vector2 input)
    {
        if (input.magnitude == 0)
        {
            if (character.Weapon != null && character.Weapon.IsAttacking)
                character.Weapon.StopAttack();
        }
        else
        {        
            character.Weapon.AttackDirection = input;
            
            if (character.Weapon != null && !character.Weapon.IsAttacking)
                character.Weapon.StartAttack();
        }

    }
}

