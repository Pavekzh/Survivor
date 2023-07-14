using UnityEngine;
using Fusion;

public class PlayerFactory : MonoBehaviour
{
    public Character Create(NetworkRunner network, Character characterPrefab,float spawnRadius)
    {
        Vector3 pos = Vector3.zero;
        pos.x = Random.Range(-spawnRadius, spawnRadius);
        pos.y = Random.Range(-spawnRadius, spawnRadius);


        return network.Spawn(characterPrefab.gameObject, pos, characterPrefab.transform.rotation).GetComponent<Character>();
    }
}
