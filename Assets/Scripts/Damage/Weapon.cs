using System.Collections;
using UnityEngine;

public abstract class Weapon:MonoBehaviour
{
    [SerializeField] private float reloadTime = 1;

    private bool isReloading;
    private Coroutine attackingCoroutine;

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
        while(isReloading)
            yield return null;

        while (true)
        {
            Attack(AttackDirection);
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

