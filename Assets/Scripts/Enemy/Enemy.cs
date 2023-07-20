using UnityEngine;
using Fusion;


public class Enemy : NetworkBehaviour,IPooledWaveObject,IWeaponOwner,IDamageHandler
{
    [Header("Health")]
    [SerializeField] private Health health;
    [Header("Move")]
    [SerializeField] private float moveSpeed;
    [Header("Attack")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private float rangeOffset;

    public string ID => gameObject.name;
    public string Killer { get; set; }

    public Vector2 ColliderSize { get; private set; }

    public Health Health { get => health; }
    public float MoveSpeed { get => moveSpeed; }
    public Weapon Weapon { get => weapon; }
    public float AttackRange { get => weapon.WeaponRange - rangeOffset; }

    //IPooledWaveObject
    public bool InPool { get; set; }
    public bool IsActive { get; private set; }
    public string Type { get; set; }

    private StateMachine<EnemyState> stateMachine;
    public EnemyDeathState DeathState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyMoveState MoveState { get; private set; }

    public Bounds MoveBoundaries { get; private set; }    
    public ScoreCounter ScoreCounter { get; private set; }
    public WaveSystem WaveSystem { get; private set; }
    private TargetDesignator targetDesignator;

    public void InitDependecies(TargetDesignator targetDesignator,Bounds moveBoundaries,WaveSystem waveSystem,ScoreCounter scoreCounter,Transform parent)
    {
        this.targetDesignator = targetDesignator;
        this.MoveBoundaries = moveBoundaries;
        this.WaveSystem = waveSystem;
        this.WaveSystem.OnWaveEnd += WaveEnded;
        this.ScoreCounter = scoreCounter;
        this.transform.parent = parent;

        if (!InPool)
            waveSystem.Pool.AddRemoteCreated(this);        
        
        stateMachine = new StateMachine<EnemyState>();
        DeathState = new EnemyDeathState(this, stateMachine);
        AttackState = new EnemyAttackState(this, stateMachine);
        MoveState = new EnemyMoveState(this, stateMachine);

        ColliderSize = GetComponent<BoxCollider2D>().bounds.size;

        stateMachine.Init(MoveState);
    }

    private void Update()
    {
        Character target;
        if(targetDesignator.GetTarget(transform.position,out target))
        {
            Vector2 playerRelativePos = target.transform.position - transform.position;
            stateMachine.CurrentState.HandleCharacterPosition(playerRelativePos);
        }

    }

    public void HandleDamage(float damage, string sender)
    {
        RPC_TakeDamage(damage,sender);     
    }

    public void OnGet(Vector2 position)
    {
        if (HasStateAuthority)
            RPC_GetFromPool(position);

        transform.position = new Vector3(position.x, position.y, transform.position.z);
        IsActive = true;
        gameObject.SetActive(true);

        if (stateMachine != null)
            stateMachine.CurrentState.Recover();
    }

    public void OnRelease()
    {
        IsActive = false;
        gameObject.SetActive(false);
    }

    private void WaveEnded()
    {
        stateMachine.CurrentState.Kill();
    }

    [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
    private void RPC_TakeDamage(float damage, string sender)
    {
        stateMachine.CurrentState.HandleDamage(damage, sender);
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All, InvokeLocal = false, TickAligned = false)]
    private void RPC_GetFromPool(Vector2 position)
    {
        OnGet(position);
    }

}
