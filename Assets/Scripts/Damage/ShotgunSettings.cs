using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunSettings", menuName = "ScriptableObjects/ShotgunSettings")]
public class ShotgunSettings : GunSettings
{
    [SerializeField] private int bulletsPerShot = 3;
    [SerializeField] private float spread = 30;

    public int BulletsPerShot { get => bulletsPerShot; }
    public float Spread { get => spread; }

    public override Gun InstantiateGun(GameObject owner)
    {
        Shotgun gun = owner.AddComponent<Shotgun>();
        gun.InitSettings(this as GunSettings);
        gun.InitSettings(this);

        return gun;
    }
}