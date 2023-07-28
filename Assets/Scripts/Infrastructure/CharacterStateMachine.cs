using System.Collections.Generic;

public class CharacterStateMachine:StateMachine<CharacterState>
{
    public CharacterStateMachine(Character character)
    {
        states = new List<CharacterState>()
        {
            new CharacterIdleState(character, this),
            new CharacterMoveState(character, this),
            new CharacterDeathState(character, this)
        };

        SwitchState<CharacterIdleState>();
    }
}
