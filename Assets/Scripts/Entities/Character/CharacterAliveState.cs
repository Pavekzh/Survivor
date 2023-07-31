using System;
using System.Collections;
using UnityEngine;

public abstract class CharacterAliveState : CharacterState
{
    public CharacterAliveState(Character character, StateMachine<CharacterState> stateMachine) : base(character, stateMachine) { }

    public override bool IsAlive { get => true; }

    public override void HandleDamage(float damage, string sender)
    {
        character.Health.TakeDamage(damage);

        if (character.Health.CurrentHealth == 0)
        {
            if (character.HasStateAuthority)
                character.RPC_StopAttack();
            stateMachine.SwitchState<CharacterDeathState>();
        }

    }

    public override void HandleAttackInput(Vector2 input)
    {
        if (input.magnitude == 0)
        {
            if (character.Weapon != null && character.IsAttacking)
                character.RPC_StopAttack();
        }
        else
        {
            SetLookDirection(input);

            character.AttackDirection = input;

            if (character.Weapon != null && !character.IsAttacking)
                character.RPC_StartAttack(input);
        }

    }

    protected void SetLookDirection(Vector2 direction)
    {
        if (Vector3.Cross(Vector3.up, direction).z <= 0)
            character.transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            character.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}

