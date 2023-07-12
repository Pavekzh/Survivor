using UnityEngine;

public class Magazine : Item
{    
    [SerializeField] private bool useMagazineSize = false;
    [SerializeField] private int amount;

    protected override void Execute(Collider2D founder)
    {
        Gun gun = founder.GetComponent<Character>().Weapon;

        if(gun != null)
        {
            if (useMagazineSize)
                gun.AddMagazine();
            else
                gun.AddBullets(amount);
        }

    }
}
