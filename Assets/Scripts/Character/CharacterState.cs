
public abstract class CharacterState:BaseState
{
    protected Character character;
    protected StateMachine<CharacterState> stateMachine;

    public CharacterState(Character character,StateMachine<CharacterState> stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;
    }

    public abstract void HandleInput(InputDetector inputDetector);
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
}

