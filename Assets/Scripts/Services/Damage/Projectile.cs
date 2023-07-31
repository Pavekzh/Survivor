using UnityEngine;
using UnityEngine.Pool;

public class Projectile:MonoBehaviour
{
    [SerializeField] protected float speed;

    protected float range;
    protected Rigidbody2D rigidbody;

    protected string ownerId;
    protected ObjectPool<Projectile> originPool;
    protected Vector2 launchPosition;


    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody == null)
            Debug.Log("Bullet must have Rigidbody component");
    }

    protected void Update()
    {
        if (((Vector2)transform.position - launchPosition).magnitude > range)
            StopProjectile();
    }

    public void Launch(Vector2 direction, string ownerId, ObjectPool<Projectile> originPool, float range)
    {
        this.ownerId = ownerId;
        this.range = range;
        this.launchPosition = transform.position;
        this.originPool = originPool;

        rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageHandler target = collision.gameObject.GetComponent<IDamageHandler>();
        StopProjectile();
    }


    protected void StopProjectile()
    {
        rigidbody.velocity = Vector2.zero;
        if (originPool != null)
            originPool.Release(this);
    }
}