using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 1;
    [Header("Attack")]
    [SerializeField] private Weapon weapon;
    [Header("Health")]
    [SerializeField] private Health health;

    public BoxCollider2D MoveBoundaries { get; private set; }
    public Vector2 ColliderSize { get; private set; }

    private InputDetector inputDetector;
    private StateMachine<CharacterState> stateMachine;

    public float MoveSpeed { get => moveSpeed; }    
    public Weapon Weapon { get => weapon; }
    public Health Health { get => health; }

    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public DeathState DeathState { get; private set; }

    public bool IsAlive { get => stateMachine.CurrentState.IsAlive(); }

    public void InitDependencies(InputDetector inputDetector,BoxCollider2D moveBoundaries)
    {
        this.inputDetector = inputDetector;
        this.MoveBoundaries = moveBoundaries;
    }

    private void Start()
    {
        stateMachine = new StateMachine<CharacterState>();

        IdleState = new IdleState(this, stateMachine);
        MoveState = new MoveState(this, stateMachine);
        DeathState = new DeathState(this, stateMachine);

        ColliderSize = GetComponent<BoxCollider2D>().bounds.size;
         
        stateMachine.Init(IdleState);
    }

    private void Update()
    {
        Vector2 moveInput = inputDetector.GetMoveInput();
        Vector2 attackInput = inputDetector.GetAttackInput();

        stateMachine.CurrentState.HandleMoveInput(moveInput);
        stateMachine.CurrentState.HandleAttackInput(attackInput);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        stateMachine.CurrentState.TriggerEnter(other);
    }
}
