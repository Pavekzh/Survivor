using UnityEngine;

public abstract class EnemyAliveState:EnemyState
{
    protected EnemyAliveState(Enemy enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine) { }

    public override bool IsAlive { get => true; }

    public override void Kill()
    {
        stateMachine.ChangeState(enemy.DeathState);
    }

    public override void HandleDamage(float damage,string sender)
    {
        float taked = enemy.Health.TakeDamage(damage);

        if (enemy.Health.CurrentHealth == 0)
        {
            enemy.Killer = sender;
            stateMachine.ChangeState(enemy.DeathState);
        }

        enemy.ScoreCounter.AddDamage(taked,sender);
    }
    public override void Recover() { }
}

