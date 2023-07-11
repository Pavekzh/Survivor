using UnityEngine;

public abstract class EnemyAliveState:EnemyState
{
    protected EnemyAliveState(Enemy enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine) { }

    public override bool IsAlive { get => true; }

    public override void HandleWaveEnd()
    {
        stateMachine.ChangeState(enemy.DeathState);
    }

    public override void HandleTakeDamage()
    {
        if (enemy.Health.CurrentHealth == 0)
            stateMachine.ChangeState(enemy.DeathState);
    }
    public override void Recover() { }
}

