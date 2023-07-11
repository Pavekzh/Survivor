using UnityEngine;

public abstract class EnemyAliveState:EnemyState
{
    protected EnemyAliveState(Enemy enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine) { }

    public override bool IsAlive { get => true; }

    public override void HandleWaveEnd()
    {
        stateMachine.ChangeState(enemy.DeathState);
    }

    public override void HandleTakeDamage(float damage,string sender)
    {
        if (enemy.Health.CurrentHealth == 0)
        {
            enemy.Killer = sender;
            stateMachine.ChangeState(enemy.DeathState);
        }


        enemy.scoreCounter.AddDamage(damage,sender);
    }
    public override void Recover() { }
}

