using UnityEngine.Pool;
using UnityEngine;

public interface IPooledObject
{
    GameObject GameObject { get; }
    ObjectPool<IPooledObject> OriginPool { get; set; }

    void OnGet();
    void OnRelease();
}
