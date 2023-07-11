using UnityEngine;

public class SingleplayerSpawnObjectFactory : SpawnObjectFactory
{
    public override GameObject Create(GameObject prefab,Transform parent)
    {
        GameObject created = Instantiate(prefab, parent);
        return created;
    }
}

