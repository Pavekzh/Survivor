using UnityEngine;
using UnityEngine.Pool;

public class Bullet:Damager
{
    [SerializeField] private float speed;

    private ObjectPool<Bullet> originPool;
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody == null)
            Debug.Log("Bullet must have Rigidbody component");
    }

    public void Launch(Vector2 direction, ObjectPool<Bullet> originPool)
    {
        this.originPool = originPool;
        rigidbody.AddForce(direction * speed,ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(originPool != null)
            originPool.Release(this);
    }

}

