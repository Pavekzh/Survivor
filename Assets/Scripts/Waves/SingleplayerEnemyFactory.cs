using UnityEngine;

public class SingleplayerEnemyFactory : EnemyFactory
{
    [SerializeField] private SingleplayerSpawnObjectFactory commonFactory;

    public override GameObject Create(GameObject prefab, Transform parent)
    {
        GameObject created = commonFactory.Create(prefab, parent);
        Enemy enemy = created.GetComponent<Enemy>();

        if (enemy == null)
            Debug.LogError("Enemy prefab must have enemy component");

        bootstrap.InitEnemy(enemy);

        return created;
    }
}
