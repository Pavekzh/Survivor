
public class StateMachine<T> where T:BaseState
{
    public T CurrentState { get; private set; }

    public void Init(T state)
    {
        CurrentState = state;
        CurrentState.Enter();
    }

    public void ChangeState(T state)
    {
        CurrentState.Exit();

        CurrentState = state;
        CurrentState.Enter();    
    }
}
