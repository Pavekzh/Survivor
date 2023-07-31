using UnityEngine;
using Fusion;

public class SceletonAutoBootstrap : NetworkBehaviour
{
    [SerializeField] private Sceleton sceleton;

    private GameBootstrap bootstrap { get => GameBootstrap.Instance; }

    public override void Spawned()
    {
        bootstrap.InitSceleton(sceleton);
    }

}