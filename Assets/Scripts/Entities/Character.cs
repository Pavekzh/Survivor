using UnityEngine;
using Fusion;

public class Character : Entity<CharacterState>,IDamageHandler
{    
    [Header("Attack")]
    [SerializeField] private Gun weapon;
    [Header("Animations")]
    [SerializeField] private string runBool = "Run";
    [Header("Death")]
    [SerializeField] private GameObject deadPrefab;

    public override float AttackRange => weapon.WeaponRange;
    public override float ReloadTime => weapon.ReloadTime;

    public override string ID { get => username; }
    public bool IsAlive { get => stateMachine.CurrentState.IsAlive; }

    [Networked(OnChanged = nameof(SetWeaponDirection))][HideInInspector]public Vector2 AttackDirection { get; set; }

    public Gun Weapon { get => weapon; }

    private string username;
    private InputDetector inputDetector;    



    public override void InitDependencies(Bounds moveBoundaries)
    {
        base.InitDependencies(moveBoundaries);

        this.stateMachine.AddState(new CharacterIdleState(this, stateMachine));
        this.stateMachine.AddState(new CharacterMoveState(this, stateMachine));
        this.stateMachine.AddState(new CharacterDeathState(this, stateMachine));
        this.stateMachine.InitState<CharacterIdleState>();
    }

    public void InitDependencies(InputDetector inputDetector,string username)
    {
        this.inputDetector = inputDetector;
        this.username = username;
    }

    public override void Spawned()
    {
        if (!HasStateAuthority)
        {
            gameObject.GetComponent<NetworkTransform>().InterpolationDataSource = InterpolationDataSources.Auto;
        }
    }

    public Gun InitGun<T>() where T : Gun
    {
        this.weapon = gameObject.AddComponent<T>();
        return this.weapon;
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

    protected override void Attack()
    {
        weapon.Attack();
    }

    public override void HandleDamage(float damage, string sender)
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
        StartAttack();
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    public void RPC_StopAttack()
    {
        StopAttack();
    }

    [Rpc(sources:RpcSources.StateAuthority,targets: RpcTargets.All)]
    public void RPC_TakeDamage(float damage, string sender)
    {
        stateMachine.CurrentState.HandleDamage(damage, sender);
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    public void RPC_AnimateMove()
    {
        animator.SetBool(runBool,true);
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    public void RPC_AnimateIdle()
    {
        animator.SetBool(runBool,false);
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    public void RPC_AnimateDeath()
    {
        Instantiate(deadPrefab,transform.position,Quaternion.identity);
        gameObject.layer = deathLayer;
        transform.GetChild(0).gameObject.SetActive(false);
    }

}
