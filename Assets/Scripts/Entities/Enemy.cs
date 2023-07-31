using UnityEngine;
using Fusion;


public abstract class Enemy : Entity<EnemyState>,IPooledWaveObject,IDamageHandler
{
    [Header("Animations")]
    [SerializeField] protected string hitTrigger = "Hit";
    [SerializeField] protected string deadBool = "Dead";
    [Header("Death")]
    [SerializeField] protected float waitUntilReleaseTime = 3;
    [Header("Attack")]
    [SerializeField] protected float rangeOffset = 1;

    public virtual Vector2 TargetDirection { get; set; }

    public int DefaultLayer { get; private set; }
    public int DeathLayer { get => deathLayer; }

    public override string ID => gameObject.name;
    public string Killer { get; set; }

    public string HitTrigger { get => hitTrigger; }
    public string DeadBool { get => deadBool; }
    public float WaitUntilReleaseTime { get => waitUntilReleaseTime; }

    //IPooledWaveObject
    public bool InPool { get; set; }
    public bool IsActive { get; private set; }
    public string Type { get; set; }


    public ScoreCounter ScoreCounter { get; private set; }
    public WaveSystem WaveSystem { get; private set; }
    private TargetDesignator targetDesignator;

    public override void InitDependencies(Bounds moveBoundaries)
    {
        base.InitDependencies(moveBoundaries);
        this.stateMachine.AddState(new EnemyMoveState(this, stateMachine));
        this.stateMachine.AddState(new EnemyAttackState(this, stateMachine));
        this.stateMachine.AddState(new EnemyDeathState(this, stateMachine));
        this.stateMachine.InitState<EnemyMoveState>();
    }

    public void InitDependencies(TargetDesignator targetDesignator,WaveSystem waveSystem,ScoreCounter scoreCounter,Transform parent)
    {
        this.DefaultLayer = gameObject.layer;

        this.targetDesignator = targetDesignator;
        this.WaveSystem = waveSystem;
        this.WaveSystem.OnWaveEnd += WaveEnded;
        this.ScoreCounter = scoreCounter;
        this.transform.parent = parent;

        if (!InPool)
            waveSystem.Pool.AddRemoteCreated(this);
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

    public override void HandleDamage(float damage, string sender)
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

        Health.RecoverHealth();
        IsAttacking = false;
        IsReloading = false;
        StopAllCoroutines();
        attackingCoroutine = null;
        

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
