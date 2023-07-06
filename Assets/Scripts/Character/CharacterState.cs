using UnityEngine;

public abstract class CharacterState:BaseState
{
    protected Character character;
    protected StateMachine<CharacterState> stateMachine;

    public CharacterState(Character character,StateMachine<CharacterState> stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;
    }

    public abstract bool IsAlive();
    public abstract void TriggerEnter(Collider trigger);
    public abstract void HandleMoveInput(Vector2 input);
    public abstract void HandleAttackInput(Vector2 input);
}


