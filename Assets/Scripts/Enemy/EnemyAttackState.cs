using System;
using UnityEngine;


public class EnemyAttackState : EnemyAliveState
{
    public EnemyAttackState(Enemy enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Enemy Attack enter");
    }

    public override void Exit()
    {
        Debug.Log("Enemy Attack exit");
    }

    public override void HandleCharacterPosition(Vector2 relativePosition)
    {
        if (relativePosition.magnitude < enemy.Weapon.WeaponRange)
            throw new NotImplementedException();
        else
            stateMachine.ChangeState(enemy.MoveState);
    }

}

