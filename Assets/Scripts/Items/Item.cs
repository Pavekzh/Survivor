using UnityEngine;
using UnityEngine.Pool;

public abstract class Item : MonoBehaviour,IPooledWaveObject
{
    [SerializeField] private LayerMask canPickUpLayers;

    public bool IsActive { get; set; }
    public string Type { get; set; }
    public bool InPool { get; set; }

    protected WaveSystem waveSystem;

    public void InitDependencies(WaveSystem waveSystem,Transform parent)
    {
        this.waveSystem = waveSystem;
        this.transform.parent = parent;

        if(!InPool)
            this.waveSystem.Pool.AddRemoteCreated(this);
    }

    protected abstract void Execute(Collider2D founder);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool canPickUp = canPickUpLayers == (canPickUpLayers | (1 << collision.gameObject.layer));

        if (!canPickUp)
            return;

        Execute(collision);
        waveSystem.Pool.Release(this);
    }  
    
    public void OnGet()
    {
        gameObject.SetActive(true);
    }

    public void OnRelease()
    {
        gameObject.SetActive(false);
    }

    public void Locate(Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}