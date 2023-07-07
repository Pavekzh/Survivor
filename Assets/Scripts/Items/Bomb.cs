using UnityEngine;

public class Bomb : Item
{
    [SerializeField] private float damage = 20;
    [SerializeField] private float radius = 5;
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private bool proportionalDamage = true;
    [SerializeField] private float minDamage = 10;

    protected override void Execute(Collider2D founder)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, radius, targetLayers);
        foreach(Collider2D target in targets)
        {
            MakeDamage(target);
        }
    }

    private void MakeDamage(Collider2D target)
    {
        Health health = target.GetComponent<Health>();

        if (health == null)
        {
            Debug.LogError("Target must have Health component");
            return;
        }

        float damage = this.damage;

        if(proportionalDamage)
        {
            float distance = (target.transform.position - transform.position).magnitude;
            damage = Mathf.Lerp(minDamage, damage, distance / radius);
        }

        health.TakeDamage(damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.white;
    }
}