using UnityEngine;
using Fusion;
using System.Collections.Generic;

public class GunSelector : NetworkBehaviour
{
    [SerializeField] private GunFactory[] guns;

    [Networked] private int firstPlayerSelected { get; set; } = -1;
    [Networked] private int secondPlayerSelected { get; set; } = -1;

    private Character character;
    private GameBootstrap gameBootstrap;

    public void InitDependencies(Character character,GameBootstrap gameBootstrap)
    {
        this.character = character;
        this.gameBootstrap = gameBootstrap;
    }

    public override void Spawned()
    {
        if(HasStateAuthority)
        {
            firstPlayerSelected = Random.Range(0, guns.Length);
            do
            {
                secondPlayerSelected = Random.Range(0, guns.Length);
            } while (secondPlayerSelected == firstPlayerSelected);

        }
        InstantiateLocalPlayerGun(character);
    }

    public void InstantiateRemotePlayerGun(Character character)
    {
        if (HasStateAuthority)
            InstantiatePlayerGun(character, guns[secondPlayerSelected],true);
        else
            InstantiatePlayerGun(character, guns[firstPlayerSelected],true);
    }

    private void InstantiateLocalPlayerGun(Character character)
    {
        if (HasStateAuthority)
            InstantiatePlayerGun(character, guns[firstPlayerSelected],false);
        else
            InstantiatePlayerGun(character, guns[secondPlayerSelected],false);
    }

    private void InstantiatePlayerGun(Character character,GunFactory gunFactory,bool isRemotePlayer)
    {
        Gun gun;

        character.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = gunFactory.Sprite;
        gun = gunFactory.InstantiateGun(character);


        gameBootstrap.InitGunForCharacter(character, gun, isRemotePlayer);
    }

}