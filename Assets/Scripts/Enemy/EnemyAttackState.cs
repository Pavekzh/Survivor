using UnityEngine;


public class EnemyAttackState : EnemyAliveState
{
    public EnemyAttackState(Enemy enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine) { }

    public override void Enter()
    {
        enemy.Weapon.StartAttack();
    }

    public override void Exit()
    {
        enemy.Weapon.StopAttack();
    }

    public override void HandleCharacterPosition(Vector2 relativePosition)
    {
        enemy.Weapon.AttackDirection = relativePosition;

        if (relativePosition.magnitude > enemy.Weapon.WeaponRange)
            stateMachine.SwitchState<EnemyMoveState>();
    }

}

