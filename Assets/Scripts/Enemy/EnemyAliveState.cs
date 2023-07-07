using UnityEngine;

public abstract class EnemyAliveState:EnemyState
{
    protected EnemyAliveState(Enemy enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine) { }

    public override bool IsAlive { get => true; }

    public override void Hit(Collider2D hitter)
    {
        if(enemy.Health.IsDamager(hitter.gameObject))
        {
            Damager damager = hitter.GetComponent<Damager>();

            enemy.Health.TakeDamage(damager);
            if (enemy.Health.CurrentHealth == 0)
                stateMachine.ChangeState(enemy.DeathState);
        }
    }
}

