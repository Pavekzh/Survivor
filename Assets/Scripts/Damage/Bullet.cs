using UnityEngine;
using UnityEngine.Pool;

public class Bullet:MonoBehaviour
{    
    [SerializeField] private float speed;

    private float damage;
    private Rigidbody2D rigidbody;

    private string ownerId;
    private ObjectPool<Bullet> originPool;
    private float range;
    private Vector2 launchPosition;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody == null)
            Debug.Log("Bullet must have Rigidbody component");
    }

    bool debug1 = false;
    bool debug2 = false;

    private void Update()
    {
        if (((Vector2)transform.position - launchPosition).magnitude > range)
            StopBullet();
    }

    public void Launch(Vector2 direction,string ownerId, ObjectPool<Bullet> originPool,float damage,float range)
    {
        this.ownerId = ownerId;
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

    protected virtual void MakeDamage(GameObject obj)
    {
        IDamageHandler target = obj.GetComponent<IDamageHandler>();
        if (target != null)
            target.HandleDamage(damage, ownerId);
    }

    private void StopBullet()
    {
        rigidbody.velocity = Vector2.zero;
        if (debug1 == true && debug2 == true)
            Debug.Log("range and trigger");
        if (originPool != null)
            originPool.Release(this);
    }
}

