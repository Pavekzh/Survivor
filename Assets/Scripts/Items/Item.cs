using UnityEngine;
using UnityEngine.Pool;

public abstract class Item : MonoBehaviour,IPooledObject
{
    [SerializeField] private LayerMask canPickUpLayers;

    public ObjectPool<IPooledObject> OriginPool { get; set; }

    public GameObject GameObject => gameObject;

    protected abstract void Execute(Collider2D founder);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool canPickUp = canPickUpLayers == (canPickUpLayers | (1 << collision.gameObject.layer));

        if (!canPickUp)
            return;

        Execute(collision);
        //pool release
        OriginPool.Release(this);
    }  
    
    public void OnGet()
    {
        gameObject.SetActive(true);
    }

    public void OnRelease()
    {
        gameObject.SetActive(false);
    }


}