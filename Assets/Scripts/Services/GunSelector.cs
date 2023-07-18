using UnityEngine;
using Fusion;

public class GunSelector : NetworkBehaviour
{
    [SerializeField] private GunFactory[] guns;

    private System.Collections.Generic.List<int> freeGuns;
    [Networked] private int firstPlayerSelected { get; set; } = -1;
    [Networked] private int secondPlayerSelected { get; set; } = -1;

    private System.Collections.Generic.List<int> FreeGuns
    {
        get
        {
            if(freeGuns == null)
            {
                freeGuns = new System.Collections.Generic.List<int>();
                for(int i = 0; i< guns.Length; i++)
                {
                    freeGuns.Add(i);
                }
            }
            return freeGuns;
        }
    }

    private Character character;
    private Transform bulletsParent;

    public void InitDependencies(Character character,Transform bulletsParent)
    {
        this.character = character;
        this.bulletsParent = bulletsParent;
    }

    public override void Spawned()
    {
        if(HasStateAuthority)
        {
            firstPlayerSelected = SelectRandomGun();
            secondPlayerSelected = SelectRandomGun();
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

    private int SelectRandomGun()
    {
        if(FreeGuns.Count != 0)
        {
            int random = Random.Range(0, FreeGuns.Count);
            int selected = freeGuns[random];
            FreeGuns.RemoveAt(random);
            return selected;
        }
        else
        {
            throw new System.IndexOutOfRangeException("Players count greater than gun variants");
        }
    }

    private void InstantiatePlayerGun(Character character,GunFactory gunFactory,bool useBlankBullets)
    {
        Gun gun;

        character.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = gunFactory.Sprite;
        gun = gunFactory.InstantiateGun(character.gameObject);

        character.InitDependencies(gun);
        gun.InitDependencies(character);
        gun.InitDependencies(bulletsParent,useBlankBullets);
    }

}