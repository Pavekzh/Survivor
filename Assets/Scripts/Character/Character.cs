using UnityEngine;

public class Character : MonoBehaviour,IWeaponOwner
{    
    [Header("Health")]
    [SerializeField] private Health health;
    [Header("Move")]
    [SerializeField] private float moveSpeed = 1;
    [Header("Attack")]
    [SerializeField] private Gun weapon;

    public Vector2 ColliderSize { get; private set; }
    public string ID { get => "Player1"; }
    public bool IsAlive { get => stateMachine.CurrentState.IsAlive; }

    public float MoveSpeed { get => moveSpeed; }    
    public Gun Weapon { get => weapon; }
    public Health Health { get => health; }

    private StateMachine<CharacterState> stateMachine;
    public CharacterIdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public CharacterDeathState DeathState { get; private set; }

    private InputDetector inputDetector;    
    public Bounds MoveBoundaries { get; private set; }

    public void InitDependencies(InputDetector inputDetector,Bounds moveBoundaries,Gun weapon)
    {
        this.inputDetector = inputDetector;
        this.MoveBoundaries = moveBoundaries;
        this.weapon = weapon;
    }

    private void Start()
    {
        stateMachine = new StateMachine<CharacterState>();

        IdleState = new CharacterIdleState(this, stateMachine);
        MoveState = new MoveState(this, stateMachine);
        DeathState = new CharacterDeathState(this, stateMachine);

        health.OnTakedDamage += TakedDamage;
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
    
    private void TakedDamage(float damage,string sender)
    {
        stateMachine.CurrentState.HandleTakeDamage();
    }

}
