using System;
using UnityEngine;

public class EnemyDeathState:EnemyState
{
    public EnemyDeathState(Enemy enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine) { }

    public override bool IsAlive { get => false; }

    public override void Enter()
    {
        //animation
        //wait
        enemy.OriginPool.Release(enemy);
    }

    public override void Exit()
    {
        enemy.Health.RecoverHealth();
        enemy.Weapon.RecoverWeapon();
    }

    public override void HandleCharacterPosition(Vector2 relativePosition) { }

    public override void HandleTakeDamage() { }

    public override void HandleWaveEnd() { }

    public override void Recover()
    {
        stateMachine.ChangeState(enemy.MoveState);
    }
}

