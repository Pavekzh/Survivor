using UnityEngine;
using Fusion;


public class Enemy : NetworkBehaviour,IPooledWaveObject,IWeaponOwner
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
    public bool IsActive { get; set; }
    public string Type { get; set; }

    private StateMachine<EnemyState> stateMachine;
    public EnemyDeathState DeathState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyMoveState MoveState { get; private set; }


    public Bounds MoveBoundaries { get; private set; }    
    public ScoreCounter ScoreCounter { get; private set; }
    public WaveSystem WaveSystem { get; private set; }

    private Character player;

    public void InitDependecies(Character character,Bounds moveBoundaries,WaveSystem waveSystem,ScoreCounter scoreCounter,Transform parent)
    {
        this.player = character;
        this.MoveBoundaries = moveBoundaries;
        this.WaveSystem = waveSystem;
        this.WaveSystem.OnWaveEnd += WaveEnded;
        this.ScoreCounter = scoreCounter;
        this.transform.parent = parent;

        if (!InPool)
            waveSystem.Pool.AddRemoteCreated(this);
    }

    public override void Spawned()
    {
        if(HasStateAuthority)
            GetComponent<NetworkTransform>().InterpolationDataSource = InterpolationDataSources.NoInterpolation;
    }

    private void Start()
    {
        stateMachine = new StateMachine<EnemyState>();
        DeathState = new EnemyDeathState(this, stateMachine);
        AttackState = new EnemyAttackState(this, stateMachine);
        MoveState = new EnemyMoveState(this, stateMachine);

        ColliderSize = GetComponent<BoxCollider2D>().bounds.size;
        health.OnTakedDamage += TakedDamage;

        stateMachine.Init(MoveState);
    }

    private void Update()
    {
        if (player.IsAlive && HasStateAuthority)
        {
            Vector2 playerRelativePos = player.gameObject.transform.position - transform.position;

            stateMachine.CurrentState.HandleCharacterPosition(playerRelativePos);
        }
    }        
    
    private void WaveEnded()
    {
        if(HasStateAuthority)
            stateMachine.CurrentState.HandleWaveEnd();
    }
    
    private void TakedDamage(float damage,string senderId)
    {
        if(HasStateAuthority)
            stateMachine.CurrentState.HandleTakeDamage(damage,senderId);
    }

    public void OnGet()
    {
        gameObject.SetActive(true);

        if (stateMachine != null)
            stateMachine.CurrentState.Recover();
    }

    public void OnRelease()
    {
        gameObject.SetActive(false);
    }

    public void Locate(Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
