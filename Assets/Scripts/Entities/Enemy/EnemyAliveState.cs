using UnityEngine;

public abstract class EnemyAliveState:EnemyState
{
    protected EnemyAliveState(Enemy enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine) { }

    public override bool IsAlive { get => true; }

    public override void Kill()
    {
        stateMachine.SwitchState<EnemyDeathState>();
    }

    public override void HandleDamage(float damage,string sender)
    {
        float taked = enemy.Health.TakeDamage(damage);
        enemy.Animator.SetTrigger(enemy.HitTrigger);

        if (enemy.Health.CurrentHealth == 0)
        {
            enemy.Killer = sender;
            stateMachine.SwitchState<EnemyDeathState>();
        }

        enemy.ScoreCounter.AddDamage(taked,sender);
    }
    public override void Recover() { }
}

