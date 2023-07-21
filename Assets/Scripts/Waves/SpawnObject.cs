using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "SpawnObject",menuName = "ScriptableObjects/SpawnObject")]
public class SpawnObject : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private string type;

    public string Type { get => type; private set => type = value; }

    private WaveObjectFactory factory;

    public void InitDependencies(WaveObjectFactory factory)
    {
        this.factory = factory;
    }    

    public IPooledWaveObject Spawn()
    {
        IPooledWaveObject result = factory.Create(prefab).GetComponent<IPooledWaveObject>();

        if(result == null)
            throw new System.Exception($"SpawnObject {this.name} doesn`t have IPooledWaveObject component or incorrectly spawned");

        result.Type = this.type;

        return result;
    }
}