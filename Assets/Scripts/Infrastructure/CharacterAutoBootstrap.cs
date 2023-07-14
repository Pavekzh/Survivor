using UnityEngine;
using Fusion;

public class CharacterAutoBootstrap : NetworkBehaviour
{
    [SerializeField] private Character character;

    public override void Spawned()
    {
        GameBootstrap.Instance.InitCharacter(character);
    }

}