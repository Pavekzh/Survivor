using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "SpawnObject",menuName = "ScriptableObjects/SpawnObject")]
public class SpawnObject : ScriptableObject
{
    [SerializeField] private GameObject prefab;

    private DefaultWaveObjectFactory factory;
    private ObjectPool<IPooledObject> pool;

    public ObjectPool<IPooledObject> Pool
    {
        get
        {
            if (pool == null)
                pool = new ObjectPool<IPooledObject>(
                    () => 
                    {
                        IPooledObject obj = factory.Create(prefab).GetComponent<IPooledObject>();
                        obj.OriginPool = pool;
                        return obj;
                    }, 
                    obj => obj.OnGet(), 
                    obj => obj.OnRelease());

            return pool;
        }
    }

    public void InitDependencies(DefaultWaveObjectFactory factory)
    {
        this.factory = factory;
    }
}