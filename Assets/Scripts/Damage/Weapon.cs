using System.Collections;
using UnityEngine;

public abstract class Weapon:MonoBehaviour
{
    [SerializeField] private float reloadTime = 1;
    [SerializeField] private float weaponRange = 5;

    protected bool isReloading;
    private Coroutine attackingCoroutine;

    public float WeaponRange { get => weaponRange; }
    public bool IsAttacking { get; private set; }
    public Vector2 AttackDirection { get; set; }

    protected abstract void Attack(Vector2 direction);

    public void StartAttack()
    {
        if(IsAttacking == false)
        {
            attackingCoroutine = StartCoroutine(Attacking());
            IsAttacking = true;
        }
    }

    public void StopAttack()
    {
        if(IsAttacking == true)
        {
            StopCoroutine(attackingCoroutine);
            attackingCoroutine = null;
            IsAttacking = false;
        }
    }

    private IEnumerator Attacking()
    {
        yield return null;

        while(isReloading)
            yield return null;

        while (true)
        {
            Attack(AttackDirection.normalized);
            yield return StartCoroutine(Reloading());
        }
    }

    private IEnumerator Reloading()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }
}

