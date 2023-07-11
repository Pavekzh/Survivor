using UnityEngine;

public abstract class SpawnObjectFactory : MonoBehaviour
{
    public abstract GameObject Create(GameObject prefab,Transform parent);

}