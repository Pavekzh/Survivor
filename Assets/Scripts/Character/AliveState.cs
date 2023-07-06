using System;
using UnityEngine;

public abstract class AliveState:CharacterState
{
    public AliveState(Character character, StateMachine<CharacterState> stateMachine) : base(character, stateMachine) { }

    protected void Move(Vector2 direction)
    {
        character.transform.Translate(direction * character.MoveSpeed * Time.deltaTime);
    }

    protected void Attack(Vector2 target)
    {
        throw new NotImplementedException();
    }
}

