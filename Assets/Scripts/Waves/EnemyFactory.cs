using UnityEngine;

public class EnemyFactory:DefaultWaveObjectFactory
{
    protected GameBootstrap bootstrap;

    public void InitDependencies(GameBootstrap bootstrap)
    {
        this.bootstrap = bootstrap;
    }

    public override GameObject Create(GameObject prefab)
    {
        Enemy enemy = base.Create(prefab).GetComponent<Enemy>();

        if (enemy == null)
            Debug.LogError("Enemy prefab must have enemy component");

        bootstrap.InitEnemy(enemy);

        return enemy.gameObject;
    }
}