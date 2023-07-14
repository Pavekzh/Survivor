using UnityEngine;

[CreateAssetMenu(fileName = "GunSettings",menuName ="ScriptableObjects/GunSettings")]
public class GunSettings : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private float reloadTime = 1;
    [SerializeField] private float weaponRange = 5;
    [SerializeField] private float damage = 10;
    [SerializeField] private int magazineSize = 15;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int defaultPoolSize = 10;

    public Sprite Sprite { get => sprite; }
    public float ReloadTime { get => reloadTime; }
    public float WeaponRange { get => weaponRange;  }
    public float Damage { get => damage; }
    public int MagazineSize { get => magazineSize;  }
    public GameObject BulletPrefab { get => bulletPrefab; }
    public int DefaultPoolSize { get => defaultPoolSize;  }

    public virtual Gun InstantiateGun(GameObject owner)
    {
        Gun instance = owner.AddComponent<Gun>();
        instance.InitSettings(this);
        return instance;
    }
}
