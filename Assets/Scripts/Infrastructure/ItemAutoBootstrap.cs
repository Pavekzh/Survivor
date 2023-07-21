using UnityEngine;
using Fusion;

public class ItemAutoBootstrap:NetworkBehaviour
{
    [SerializeField] private Item item;

    private GameBootstrap bootstrap { get => GameBootstrap.Instance; }

    public override void Spawned()
    {
        bootstrap.InitItem(item);
    }
}