using UnityEngine;
using UnityEngine.Pool;

public class Bullet:Damager
{
    [SerializeField] private float speed;

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

    public void Launch(Vector2 direction, ObjectPool<Bullet> originPool,float range)
    {
        this.originPool = originPool;
        this.launchPosition = transform.position;
        this.range = range;
        rigidbody.AddForce(direction * speed,ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopBullet();
    }

    private void StopBullet()
    {
        rigidbody.velocity = Vector2.zero;
        if (originPool != null)
            originPool.Release(this);
    }
}

