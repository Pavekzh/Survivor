using UnityEngine;

public abstract class EnemyState : BaseState
{
    protected Enemy enemy;
    protected StateMachine<EnemyState> stateMachine;

    public EnemyState(Enemy enemy,StateMachine<EnemyState> stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public abstract bool IsAlive { get; }

    public abstract void HandleCharacterPosition(Vector2 relativePosition);
    public abstract void HadleTakeDamage();


}
