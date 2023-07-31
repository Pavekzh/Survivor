using UnityEngine;

[CreateAssetMenu(fileName = "GunSettings",menuName ="ScriptableObjects/GunSettings")]
public class GunFactory : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private float reloadTime = 1;
    [SerializeField] private float weaponRange = 5;
    [SerializeField] private int magazineSize = 15;
    [SerializeField] private Projectile bulletPrefab;
    [SerializeField] private Projectile blankBulletPrefab;
    [SerializeField] private int defaultPoolSize = 10;

    public Sprite Sprite { get => sprite; }
    public float ReloadTime { get => reloadTime; }
    public float WeaponRange { get => weaponRange;  }
    public int MagazineSize { get => magazineSize;  }
    public Projectile BulletPrefab { get => bulletPrefab; }
    public Projectile BlankBulletPrefab { get => blankBulletPrefab; }
    public int DefaultPoolSize { get => defaultPoolSize;  }

    public virtual Gun InstantiateGun(Character owner)
    {
        Gun instance = owner.InitGun<Gun>();
        instance.InitSettings(this);
        return instance;
    }
}
