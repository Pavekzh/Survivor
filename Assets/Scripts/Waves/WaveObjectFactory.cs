using UnityEngine;
using Fusion;

public class WaveObjectFactory:MonoBehaviour
{
    protected NetworkRunner network;

    public void InitDependencies(NetworkRunner network)
    {
        this.network = network;
    }

    public virtual GameObject Create(GameObject prefab)
    {
        return network.Spawn(prefab).gameObject;
    }
}