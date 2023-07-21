using UnityEngine;
using Fusion;

public class CharacterAutoBootstrap : NetworkBehaviour
{
    [SerializeField] private Character character;

    private GameBootstrap bootstrap { get => GameBootstrap.Instance; }

    public override void Spawned()
    {
        bootstrap.InitCharacter(character);
    }

}