using UnityEngine;

public class EnemyMoveState : EnemyAliveState
{
    public EnemyMoveState(Enemy enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine) { }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void HandleCharacterPosition(Vector2 relativePosition)
    {
        if (relativePosition.magnitude > enemy.AttackRange)
            Move(relativePosition.normalized);
        else
            stateMachine.SwitchState<EnemyAttackState>();
    }

    protected void Move(Vector2 direction)
    {
        Vector2 newPosition = (Vector2)(enemy.transform.position) + (direction * enemy.MoveSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(newPosition.x, enemy.MoveBoundaries.min.x + enemy.ColliderSize.x, enemy.MoveBoundaries.max.x - enemy.ColliderSize.x);
        float clampedY = Mathf.Clamp(newPosition.y, enemy.MoveBoundaries.min.y + enemy.ColliderSize.y, enemy.MoveBoundaries.max.y - enemy.ColliderSize.y);

        enemy.transform.position = new Vector3(clampedX, clampedY, enemy.transform.position.z);

    }
}
