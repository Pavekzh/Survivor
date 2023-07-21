using UnityEngine;
using Fusion;

public abstract class Item : NetworkBehaviour,IPooledWaveObject
{
    [SerializeField] private LayerMask canPickUpLayers;

    public bool IsActive { get; private set; }
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
    
    public void OnGet(Vector2 position)
    {
        if (HasStateAuthority)
            RPC_GetFromPool(position);

        transform.position = new Vector3(position.x, position.y, transform.position.z);
        IsActive = true;
        gameObject.SetActive(true);
    }

    public void OnRelease()
    {
        IsActive = false;
        gameObject.SetActive(false);
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All, InvokeLocal = false,TickAligned = false)]
    private void RPC_GetFromPool(Vector2 position)
    {
        OnGet(position);
    }

    
}