using UnityEngine;
using Fusion;

public class EnemyAutoBootstrap:NetworkBehaviour
{
    [SerializeField]private Enemy enemy;

    private GameBootstrap bootstrap { get => GameBootstrap.Instance; }

    public override void Spawned()
    {
        bootstrap.InitEnemy(enemy);
    }

}