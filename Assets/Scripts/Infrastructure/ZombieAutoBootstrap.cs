using UnityEngine;
using Fusion;

public class ZombieAutoBootstrap : NetworkBehaviour
{
    [SerializeField] private Zombie zombie;

    private GameBootstrap bootstrap { get => GameBootstrap.Instance; }

    public override void Spawned()
    {
        bootstrap.InitEnemy(zombie);
    }

}