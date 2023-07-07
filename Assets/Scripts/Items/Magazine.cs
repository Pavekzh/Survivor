using UnityEngine;

public class Magazine : Item
{
    [SerializeField] private int amount;

    protected override void Execute(Collider2D founder)
    {
        Gun gun = founder.GetComponent<Gun>();

        if(gun != null)
        {
            gun.AddBullets(amount);
        }

    }
}
