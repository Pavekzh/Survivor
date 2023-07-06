using UnityEngine;
using UnityEngine.Pool;

public class Bullet:MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)    
    {
        originPool.Release(this);
    }
}

