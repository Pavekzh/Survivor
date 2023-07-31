using UnityEngine;

public class Zombie:Enemy
{
    [SerializeField] private float attackRange;
    [SerializeField] private float reloadTime;
    [SerializeField] private float damage;
    [SerializeField] private AttackMode mode;
    [SerializeField] private LayerMask targetLayers;

    public override float AttackRange => attackRange - rangeOffset;
    public override float ReloadTime => reloadTime;

    private void MakeDamageSingle(Collider2D target)
    {
        IDamageHandler handler = target.gameObject.GetComponent<IDamageHandler>();
        if (handler != null)
            handler.HandleDamage(damage, "");
    }

    private void MakeDamageMulti(Collider2D[] targets)
    {
        foreach (Collider2D target in targets)
            MakeDamageSingle(target);

    }

    protected override void Attack()
    {
        Collider2D[] hurtColliders = Physics2D.OverlapCircleAll(transform.position, attackRange, targetLayers);

        if (hurtColliders.Length == 0)
            return;

        if (mode == AttackMode.MultiTarget)
            MakeDamageMulti(hurtColliders);
        else if (mode == AttackMode.UndefinedSingle)
            MakeDamageSingle(hurtColliders[0]);
        else
        {
            float minDistance = (hurtColliders[0].transform.position - transform.position).magnitude;
            int nearestIndex = 0;

            for (int i = 1; i < hurtColliders.Length; i++)
            {
                float distance = (hurtColliders[i].transform.position - transform.position).magnitude;
                if (distance < minDistance)
                    nearestIndex = i;
            }

            MakeDamageSingle(hurtColliders[nearestIndex]);

        }

    }
}