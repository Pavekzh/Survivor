using UnityEngine;
using UnityEngine.Pool;

public class Bullet:Projectile
{
    [SerializeField] private float damage;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageHandler target = collision.gameObject.GetComponent<IDamageHandler>();
        MakeDamage(target);
        StopProjectile();
    }

    protected void MakeDamage(IDamageHandler handler)
    {
        if (handler != null)
            handler.HandleDamage(damage, ownerId);
    }

}

