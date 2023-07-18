﻿using System;
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
        enemy.Killer = null;
    }

    public override void HandleCharacterPosition(Vector2 relativePosition) { }

    public override void HandleDamage(float damage,string sender) { }

    public override void Kill() { }

    public override void Recover()
    {        
        enemy.Health.RecoverHealth();
        enemy.Weapon.RecoverWeapon();
        stateMachine.ChangeState(enemy.MoveState);        

    }
}

