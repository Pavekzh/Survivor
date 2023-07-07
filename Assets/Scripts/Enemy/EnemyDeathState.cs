using System;
using UnityEngine;

public class EnemyDeathState:EnemyState
{
    public EnemyDeathState(Enemy enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine) { }

    public override bool IsAlive { get => false; }

    public override void Enter()
    {
        Debug.Log("Enemy Death enter");
    }

    public override void Exit()
    {
        Debug.Log("Enemy Death exit");
    }

    public override void HandleCharacterPosition(Vector2 relativePosition)
    {
        throw new NotImplementedException();
    }

    public override void HadleTakeDamage() { }
}

