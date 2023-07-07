using UnityEngine;


public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Health health;
    [Header("Move")]
    [SerializeField] private float moveSpeed;
    [Header("Attack")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private float rangeOffset;

    public Vector2 ColliderSize { get; private set; }

    public Health Health { get => health; }
    public float MoveSpeed { get => moveSpeed; }
    public Weapon Weapon { get => weapon; }
    public float AttackRange { get => weapon.WeaponRange - rangeOffset; }

    private StateMachine<EnemyState> stateMachine;
    public EnemyDeathState DeathState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyMoveState MoveState { get; private set; }

    private Character player;
    public BoxCollider2D MoveBoundaries { get; private set; }

    public void InitDependecies(Character character,BoxCollider2D moveBoundaries)
    {
        this.player = character;
        this.MoveBoundaries = moveBoundaries;
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
        if (player.IsAlive)
        {
            Vector2 playerRelativePos = player.gameObject.transform.position - transform.position;

            stateMachine.CurrentState.HandleCharacterPosition(playerRelativePos);
        }
    }    
    
    private void TakedDamage(float currentHealth)
    {
        stateMachine.CurrentState.HadleTakeDamage();
    }

}
