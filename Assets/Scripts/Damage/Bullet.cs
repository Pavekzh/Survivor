using UnityEngine;
using UnityEngine.Pool;

public class Bullet:MonoBehaviour
{    
    [SerializeField] private float speed;

    private float damage;
    private Rigidbody2D rigidbody;

    private ObjectPool<Bullet> originPool;
    private float range;
    private Vector2 launchPosition;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody == null)
            Debug.Log("Bullet must have Rigidbody component");
    }

    private void Update()
    {
        if (((Vector2)transform.position - launchPosition).magnitude > range)
            StopBullet();

    }

    public void Launch(Vector2 direction, ObjectPool<Bullet> originPool,float damage,float range)
    {
        this.damage = damage;        
        this.range = range;
        this.launchPosition = transform.position;
        this.originPool = originPool;

        rigidbody.AddForce(direction * speed,ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MakeDamage(collision.gameObject);
        StopBullet();
    }

    private void MakeDamage(GameObject obj)
    {
        Health health = obj.GetComponent<Health>();
        if(health != null)
            health.TakeDamage(damage);
    }

    private void StopBullet()
    {
        rigidbody.velocity = Vector2.zero;
        if (originPool != null)
            originPool.Release(this);
    }
}

