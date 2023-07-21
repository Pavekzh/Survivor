using System;
using UnityEngine;

public class CharacterDeathState : CharacterState
{
    public CharacterDeathState(Character character, StateMachine<CharacterState> stateMachine) : base(character, stateMachine) { }

    public override void Enter()
    {
        if (character.HasStateAuthority)
            character.RPC_AnimateDeath();
        Debug.Log("Death enter");
    }

    public override void Exit()
    {
        Debug.Log("Death exit");
    }

    public override bool IsAlive { get => false; }

    public override void HandleMoveInput(Vector2 input)
    {
        Move(input);
    }

    public override void HandleDamage(float damage,string sender) { }
    public override void HandleAttackInput(Vector2 input) { }


    protected void Move(Vector2 direction)
    {
        Vector2 newPosition = (Vector2)(character.transform.position) + (direction * character.MoveSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(newPosition.x, character.MoveBoundaries.min.x + character.ColliderSize.x, character.MoveBoundaries.max.x - character.ColliderSize.x);
        float clampedY = Mathf.Clamp(newPosition.y, character.MoveBoundaries.min.y + character.ColliderSize.y, character.MoveBoundaries.max.y - character.ColliderSize.y);

        character.transform.position = new Vector3(clampedX, clampedY, character.transform.position.z);
    }
}

