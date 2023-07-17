using UnityEngine;

public class DefaultWaveObjectFactory:MonoBehaviour
{
    protected Transform parent;

    public void InitDependencies(Transform parent)
    {
        this.parent = parent;
    }

    public virtual GameObject Create(GameObject prefab)
    {
        return GameObject.Instantiate(prefab,parent);
    }
}