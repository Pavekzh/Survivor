using Fusion;
using System.Collections;
using UnityEngine;

public abstract class Entity<T>: NetworkBehaviour where T : BaseState
{
    [Header("Health")]
    [SerializeField] protected Health health;
    [Header("Move")]
    [SerializeField] protected float moveSpeed = 1;
    [Header("Animations")]
    [SerializeField] protected Animator animator;
    [Header("Death")]
    [SerializeField] protected int deathLayer = 14;

    public abstract string ID { get; }

    public bool IsReloading { get; protected set; }
    public bool IsAttacking { get; protected set; }
    protected Coroutine attackingCoroutine;

    protected StateMachine<T> stateMachine;


    public Health Health { get => health; }
    public float MoveSpeed { get => moveSpeed; }
    public Animator Animator { get => animator; }
    public abstract float AttackRange { get; }
    public abstract float ReloadTime { get; }

    public Vector2 ColliderSize { get; private set; }
    public Bounds MoveBoundaries { get; private set; }

    public virtual void InitDependencies(Bounds moveBoundaries)
    {
        this.MoveBoundaries = moveBoundaries;

        stateMachine = new StateMachine<T>();
        ColliderSize = GetComponent<BoxCollider2D>().bounds.size;
    }

    public abstract void HandleDamage(float damage, string sender);    
    
    protected abstract void Attack();

    public void StartAttack()
    {
        if (IsAttacking == false)
        {
            attackingCoroutine = StartCoroutine(Attacking());
            IsAttacking = true;
        }
    }

    public void StopAttack()
    {
        if (IsAttacking == true)
        {

            StopCoroutine(attackingCoroutine);
            attackingCoroutine = null;
            IsAttacking = false;
        }
    }

    protected IEnumerator Attacking()
    {
        yield return null;

        while (IsReloading)
            yield return null;

        while (true)
        {
            Attack();
            yield return StartCoroutine(Reloading());
        }
    }

    protected IEnumerator Reloading()
    {
        IsReloading = true;
        yield return new WaitForSeconds(ReloadTime);
        IsReloading = false;
    }    
    
}