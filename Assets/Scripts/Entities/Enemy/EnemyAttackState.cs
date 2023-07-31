using UnityEngine;


public class EnemyAttackState : EnemyAliveState
{
    public EnemyAttackState(Enemy enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine) { }

    public override void Enter()
    {
        enemy.StartAttack();
    }

    public override void Exit()
    {
        enemy.StopAttack();
    }

    public override void HandleCharacterPosition(Vector2 relativePosition)
    {
        enemy.TargetDirection = relativePosition;

        if (relativePosition.magnitude > enemy.AttackRange)
            stateMachine.SwitchState<EnemyMoveState>();
    }

}

