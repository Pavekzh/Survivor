using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 1;

    private InputDetector inputDetector;
    private StateMachine<CharacterState> stateMachine;

    public float MoveSpeed { get => moveSpeed; }

    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }

    public void InitDependencies(InputDetector inputDetector)
    {
        this.inputDetector = inputDetector;
    }

    private void Start()
    {
        stateMachine = new StateMachine<CharacterState>();
        IdleState = new IdleState(this, stateMachine);
        MoveState = new MoveState(this, stateMachine);

        stateMachine.Init(IdleState);
    }

    private void Update()
    {
        stateMachine.CurrentState.HandleInput(inputDetector);
        stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
}
