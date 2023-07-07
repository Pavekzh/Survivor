using UnityEngine;

public class EnemyMoveState : EnemyAliveState
{
    public EnemyMoveState(Enemy enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Enemy Move enter");
    }

    public override void Exit()
    {
        Debug.Log("Enemy Move exit");
    }

    public override void HandleCharacterPosition(Vector2 relativePosition)
    {
        if (relativePosition.magnitude > enemy.AttackRange)
            Move(relativePosition.normalized);
        else
            stateMachine.ChangeState(enemy.AttackState);
    }

    protected void Move(Vector2 direction)
    {
        Vector2 newPosition = (Vector2)(enemy.transform.position) + (direction * enemy.MoveSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(newPosition.x, enemy.MoveBoundaries.bounds.min.x + enemy.ColliderSize.x, enemy.MoveBoundaries.bounds.max.x - enemy.ColliderSize.x);
        float clampedY = Mathf.Clamp(newPosition.y, enemy.MoveBoundaries.bounds.min.y + enemy.ColliderSize.y, enemy.MoveBoundaries.bounds.max.y - enemy.ColliderSize.y);

        enemy.transform.position = new Vector3(clampedX, clampedY, enemy.transform.position.z);
    }
}
