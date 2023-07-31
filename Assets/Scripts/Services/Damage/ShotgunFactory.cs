using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunSettings", menuName = "ScriptableObjects/ShotgunSettings")]
public class ShotgunFactory : GunFactory
{
    [SerializeField] private int bulletsPerShot = 3;
    [SerializeField] private float spread = 30;

    public int BulletsPerShot { get => bulletsPerShot; }
    public float Spread { get => spread; }

    public override Gun InstantiateGun(Character owner)
    {
        Shotgun instance = owner.InitGun<Shotgun>() as Shotgun;
        instance.InitSettings(this as GunFactory);
        instance.InitSettings(this);
        return instance;
    }
}