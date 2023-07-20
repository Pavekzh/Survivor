using UnityEngine;
using Fusion;

public class Character : NetworkBehaviour,IWeaponOwner,IDamageHandler
{    
    [Header("Health")]
    [SerializeField] private Health health;
    [Header("Move")]
    [SerializeField] private float moveSpeed = 1;
    [Header("Attack")]
    [SerializeField] private Gun weapon;

    public Vector2 ColliderSize { get; private set; }
    public string ID { get => username; }
    public bool IsAlive { get => stateMachine.CurrentState.IsAlive; }

    [Networked(OnChanged = nameof(SetWeaponDirection))]public Vector2 AttackDirection { get; set; }

    public float MoveSpeed { get => moveSpeed; }    
    public Health Health { get => health; }
    public Gun Weapon { get => weapon; }

    private StateMachine<CharacterState> stateMachine;
    public CharacterIdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public CharacterDeathState DeathState { get; private set; }

    private string username;
    private InputDetector inputDetector;    
    public Bounds MoveBoundaries { get; private set; }

    public void InitDependencies(InputDetector inputDetector,Bounds moveBoundaries,string username)
    {
        this.inputDetector = inputDetector;
        this.MoveBoundaries = moveBoundaries;
        this.username = username;

        stateMachine = new StateMachine<CharacterState>();

        IdleState = new CharacterIdleState(this, stateMachine);
        MoveState = new MoveState(this, stateMachine);
        DeathState = new CharacterDeathState(this, stateMachine);

        ColliderSize = GetComponent<BoxCollider2D>().bounds.size;

        stateMachine.Init(IdleState);
    }

    public void InitDependencies(Gun weapon)
    {
        this.weapon = weapon;
    }

    public override void Spawned()
    {
        if (!HasStateAuthority)
        {
            gameObject.GetComponent<NetworkTransform>().InterpolationDataSource = InterpolationDataSources.Auto;
        }
    }

    private void Update()
    {
        if (HasStateAuthority)
        {
            Vector2 moveInput = inputDetector.GetMoveInput();
            Vector2 attackInput = inputDetector.GetAttackInput();

            stateMachine.CurrentState.HandleMoveInput(moveInput);
            stateMachine.CurrentState.HandleAttackInput(attackInput);
        }
    }
    
    public void HandleDamage(float damage, string sender)
    {
        if (HasStateAuthority)
            RPC_TakeDamage(damage, sender);
    }

    private static void SetWeaponDirection(Changed<Character> character)
    {
        character.Behaviour.weapon.AttackDirection = character.Behaviour.AttackDirection;
    }

    [Rpc(sources: RpcSources.StateAuthority,targets: RpcTargets.All)]
    public void RPC_StartAttack(Vector2 direction)
    {
        weapon.AttackDirection = direction;
        weapon.StartAttack();
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    public void RPC_StopAttack()
    {
        weapon.StopAttack();
    }

    [Rpc(sources:RpcSources.StateAuthority,targets: RpcTargets.All)]
    public void RPC_TakeDamage(float damage, string sender)
    {
        stateMachine.CurrentState.HandleDamage(damage, sender);
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    public void RPC_AnimateMove()
    {
        Debug.Log("MoveAnimation");
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    public void RPC_AnimateIdle()
    {
        Debug.Log("IdleAnimation");
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    public void RPC_AnimateDeath()
    {
        Debug.Log("DeathAnimation");
    }

}
