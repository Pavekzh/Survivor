using System;
using System.Collections;
using UnityEngine;

public abstract class CharacterAliveState:CharacterState
{
    public CharacterAliveState(Character character, StateMachine<CharacterState> stateMachine) : base(character, stateMachine) { }

    public override bool IsAlive { get => true; }

    public override void HandleDamage(float damage,string sender)
    {
        character.Health.TakeDamage(damage);

        if (character.Health.CurrentHealth == 0)
        {
            character.RPC_StopAttack();
            stateMachine.ChangeState(character.DeathState);
        }

    }

    public override void HandleAttackInput(Vector2 input)
    {
        if (input.magnitude == 0)
        {
            if(character.Weapon != null && character.Weapon.IsAttacking)
                character.RPC_StopAttack();
        }
        else
        {        
            character.AttackDirection = input;

            if(character.Weapon != null && !character.Weapon.IsAttacking)
                character.RPC_StartAttack(input);
        }

    }
}

