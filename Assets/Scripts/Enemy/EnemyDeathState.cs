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
        enemy.WaveSystem.Pool.Release(enemy);
        if(enemy.Killer != null)
            enemy.ScoreCounter.AddKill(enemy.Killer);
    }

    public override void Exit()
    {
        enemy.Health.RecoverHealth();
        enemy.Weapon.RecoverWeapon();
        enemy.Killer = null;
    }

    public override void HandleCharacterPosition(Vector2 relativePosition) { }

    public override void HandleTakeDamage(float damage,string sender) { }

    public override void HandleWaveEnd() { }

    public override void Recover()
    {
        stateMachine.ChangeState(enemy.MoveState);
    }
}

