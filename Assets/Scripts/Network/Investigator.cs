using UnityEngine;
using Fusion;

//for testing Photon Fusion
public class Investigator : NetworkBehaviour
{
    [Networked] [SerializeField] private int number { get; set; } = 3;

    public override void Spawned()
    {
        if (HasStateAuthority)
            number = Random.Range(0, 10);

        Debug.LogError(number);
    }
}