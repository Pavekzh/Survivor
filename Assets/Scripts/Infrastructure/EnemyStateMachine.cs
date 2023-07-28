using System.Collections.Generic;

public class EnemyStateMachine : StateMachine<EnemyState>
{
    public EnemyStateMachine(Enemy enemy)
    {
        states = new List<EnemyState>()
        {
            new EnemyDeathState(enemy, this),
            new EnemyAttackState(enemy, this),
            new EnemyMoveState(enemy, this)
        };

        SwitchState<EnemyMoveState>();
    }
}
